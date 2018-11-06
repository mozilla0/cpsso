using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PrivateLabelLite.Models;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Framework.Helper;
using PrivateLabelLite.ActionFilter;
using PrivateLabelLite.Entities.EndUser;
using AutoMapper;
using PrivateLabelLite.Entities;
using PrivateLabelLite.Services.Company;
using PrivateLabelLite.Services.PartnerApi;
using PrivateLabelLite.Services.Order;
using PrivateLabelLite.Entities.Order;
using PrivateLabelLite.Services.Email;
using PrivateLabelLite.Entities.Email;
using PrivateLabelLite.Entities.Subsciptions;

namespace PrivateLabelLite.Controllers
{
    [AuthorizeReseller]
    public class CompanyController : BaseController
    {
        #region Fields
        private readonly ICompanyService _companyService;
        private readonly IPartnerApi _partnerApi;
        private readonly IOrderService _orderService;
        private readonly IEmailService _emailService;
        #endregion

        #region Ctor
        public CompanyController(ICompanyService companyService, IOrderService orderService, IPartnerApi partnerApi, IEmailService emailService)
        {
            this._companyService = companyService;
            this._partnerApi = partnerApi;
            this._orderService = orderService;
            this._emailService = emailService;
        }


        #endregion
        // GET: Company
        public ActionResult EndUserMapping()
        {
            var model = new CompanyEndUserMappingModels();
            var customerFilter = new EndCustomerFilter()
            {
                Page = 1,
                RecordsPerPage = ConfigKeys.PageSize
            };
            var customers = _companyService.GetEndCustomers(customerFilter);
            var companies = _companyService.GetCompanies(new CompanyFilter());
            if (companies != null)
            {
                model.Companies = companies.Select(x => new CompanyFilter() { CompanyName = x.CompanyName, CompanyId = x.CompanyId }).ToList();
            }
            model.EndUsers = Mapper.Map<List<EndUserInfoModel>>(customers);
            model.Filter = customerFilter;
            model.Filter.TotalRecords = model.EndUsers != null && model.EndUsers.Count > 0 ? model.EndUsers.FirstOrDefault().TotalRecords.ToString() : "0";
            return View(model);
        }

        public ActionResult Home()
        {
            SubscriptionSummaryModel model = null;
            var companies = _companyService.GetCompanies(new CompanyFilter());
            var companyFilter = new CompanyOrderFilter()
            {
                Page = 1,
                RecordsPerPage = ConfigKeys.PageSize
            };
            var subscriptionList = _companyService.GetSubscriptionDetail(companyFilter);

            if (subscriptionList != null)
            {
                model = new SubscriptionSummaryModel()
                {
                    SubscriptionList = Mapper.Map<List<SubscriptionDetailModel>>(subscriptionList.SubscriptionList),
                    Companies = companies,
                    Filter = companyFilter
                };

            }
            return View(model);
        }


        [HttpGet]
        public ActionResult SalesOrderMapping()
        {
            SubscriptionSummaryModel model = null;
            var companies = _companyService.GetCompanies(new CompanyFilter());
            companies.Insert(0, new CompanyDetail { CompanyName = "ALL" });
            var companyFilter = new CompanyOrderFilter()
            {
                Page = 1,
                RecordsPerPage = ConfigKeys.PageSize
            };
            var subscriptionList = _companyService.GetSubscriptionDetail(companyFilter);

            if (subscriptionList != null)
            {
                model = new SubscriptionSummaryModel()
                {
                    SubscriptionList = Mapper.Map<List<SubscriptionDetailModel>>(subscriptionList.SubscriptionList),
                    Companies = companies,
                    Filter = companyFilter
                };

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult GetCompanyEndUsers(EndCustomerFilter customerFilter)
        {

            var customers = _companyService.GetEndCustomers(customerFilter);
            var endUsers = Mapper.Map<List<EndUserInfoModel>>(customers);

            return Json(endUsers, JsonRequestBehavior.DenyGet);
        }

        public ActionResult GetPaginatedSubscriptions(CompanyOrderFilter filter)
        {
            var companies = _companyService.GetCompanies(new CompanyFilter());
            companies.Insert(0, new CompanyDetail { CompanyName = "ALL", CompanyId = 0 });
            var company = companies.FirstOrDefault(x => x.CompanyId == filter.CompanyId);
            if (company == null)
            {
                company = companies.FirstOrDefault(x => x.CompanyId == 0);
            }
            filter.CompanyName = company.CompanyName;
            var subscriptionList = _companyService.GetSubscriptionDetail(filter);

            SubscriptionSummaryModel model = new SubscriptionSummaryModel()
            {
                SubscriptionList = Mapper.Map<List<SubscriptionDetailModel>>(subscriptionList.SubscriptionList),
                Filter = filter
            };
            model.Filter.CompanyName = company.CompanyName;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEndUserMapping(EndCustomerFilter model)
        {
            var resp = _companyService.AddEndUserMapping(model);
            if (resp.IsValid == true && model.Id == 0)
            {

                string str = model.CompanyName;
                str = str.Replace(" ", "");
                var company = str;
                var subject = ConfigKeys.ApplicationName;
                var appUrl = ConfigKeys.AppUrl;
                //fire email
                EmailDetails data = new EmailDetails()
                {
                    To = model.Email,
                    Subject = subject,
                    Body = "Welcome to MSFT CSP Management Tool. </br> You have been granted access to the App.Please, visit the URL below and use your own MSFT credentials to start managing your different product lines.</br>" + appUrl
                };
                _emailService.NewUserNotification(data);
            }

            return Json(resp, JsonRequestBehavior.DenyGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpsertCompanyOrderMapping(CompanyOrderModels model)
        {
            var order = Mapper.Map<PrivateLabelLite.Entities.CompanyOrder>(model);
            // insert orders into database
            if (order != null)
            {

                var salesOrderIds = new List<string>();
                if (model.SalesOrderIdCSV != null && !string.IsNullOrEmpty(model.SalesOrderIdCSV))
                {
                    salesOrderIds = order.SalesOrderIdCSV.Split(',').ToList();
                    salesOrderIds.RemoveAll(x => String.IsNullOrEmpty(x));
                    salesOrderIds = salesOrderIds.Distinct().ToList();
                }
                else
                    salesOrderIds.Add(model.SalesOrderId);

                // get order details from api// order 
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                order.SalesOrderIdCSV = "";
                foreach (var orderNumber in salesOrderIds)
                {
                    try
                    {
                        var orderDetail = _partnerApi.GetOrderDetail(orderNumber).OrderInfo;
                        _orderService.RemoveUnknownMSProductsAndUpdateMissingInfoFromDb(orderDetail);
                        if (orderDetail != null)
                        {
                            orderDetails.Add(orderDetail);
                            order.SalesOrderIdCSV = !string.IsNullOrEmpty(order.SalesOrderIdCSV) ? order.SalesOrderIdCSV + ", " + orderNumber : orderNumber;
                        }
                    }
                    catch (Exception)
                    {
                        order.OrdersThatNotFound = !string.IsNullOrEmpty(order.OrdersThatNotFound) ? order.OrdersThatNotFound + ", " + orderNumber : orderNumber;
                    }

                }
                _orderService.UpdateOrdersInfo(orderDetails);

            }
            var resp = new Response()
            {
                IsValid = true,
                ErrorMessage = ""
            };
            if (!string.IsNullOrEmpty(order.SalesOrderIdCSV))
            {
                var compResponse = _companyService.UpsertCompanyOrderMapping(order);
                if (!string.IsNullOrEmpty(compResponse.ErrorMessage))
                {
                    resp.ErrorMessage = "Sales order " + compResponse.ErrorMessage + " already mapped.";
                }
                if (!string.IsNullOrEmpty(compResponse.Message))
                {
                    resp.Message = "Sales order " + compResponse.Message + " mapped successfully.";
                }
            }
            resp.ErrorMessage = (!string.IsNullOrEmpty(order.OrdersThatNotFound) ? resp.ErrorMessage + "#" + "Sales Order " + order.OrdersThatNotFound + " not found." : resp.ErrorMessage);
            return Json(resp, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult GetCompanyOrders(CompanyOrderFilter filter)
        {
            var res = Mapper.Map<List<CompanyOrderModels>>(_companyService.GetCompanyOrders(filter));
            return Json(res, JsonRequestBehavior.DenyGet);
        }

        public ActionResult SaveMapping(List<SubscriptionDetailModel> unMapped, List<SubscriptionDetailModel> mapped)
        {
            List<SubscriptionDetail> unMap = Mapper.Map<List<SubscriptionDetailModel>, List<SubscriptionDetail>>(unMapped);
            List<SubscriptionDetail> Map = Mapper.Map<List<SubscriptionDetailModel>, List<SubscriptionDetail>>(mapped);
            var result = _companyService.SaveMapping(unMap, Map);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEndUserMapping(EndCustomerFilter user)
        {
            user.CustomerId = user.CustomerId.Remove(user.CustomerId.Length - 1);
            List<decimal> customerIds = new List<decimal>();
            foreach (var item in user.CustomerId.Split(','))
            {
                customerIds.Add(Convert.ToDecimal(item));
            }
            var resp = _companyService.RemoveEndUserMapping(customerIds);
            return Json(resp, JsonRequestBehavior.DenyGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCompanyOrderMapping(string RecordIds)
        {
            RecordIds = RecordIds.Remove(RecordIds.Length - 1);
            List<decimal> recordIds = new List<decimal>();
            foreach (var item in RecordIds.Split(','))
            {
                recordIds.Add(Convert.ToDecimal(item));
            }
            var resp = _companyService.DeleteCompanyOrderMapping(recordIds);
            return Json(resp, JsonRequestBehavior.DenyGet);

        }

        [HttpPost]
        public ActionResult RefreshCompanyList()
        {
            var customer = _partnerApi.GetAllCustomers();

            var companies = new List<string>();
            if (customer != null && customer.EndCustomersDetails != null)
            {
                companies = customer.EndCustomersDetails.Select(x => x.CompanyName).Distinct().ToList();
            }
            var resp = _companyService.UpdateCompanies(companies, GetLoggedInUserInfo());
            resp.Data = _companyService.GetCompanies(new CompanyFilter());
            return Json(resp, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public ActionResult UpdateSubscription()
        {
            var response = RedirectToAction("RefereshSubscriptionDetail", "OrderAsync");
            return response;
        }
        public bool checkDatabase()
        {
            var response = _orderService.checkDatabase();
            return response;
        }
        public ActionResult EditProducts()
        {
            SubscriptionSummaryModel model = null;
            var companies = _companyService.GetCompanies(new CompanyFilter());
            companies.Insert(0, new CompanyDetail { CompanyName = "ALL" });
            var companyFilter = new CompanyOrderFilter()
            {
                Page = 1,
                RecordsPerPage = ConfigKeys.PageSize,
                EditProductStatus = false
            };
            var subscriptionList = _companyService.GetSubscriptionDetail(companyFilter);

            if (subscriptionList != null)
            {
                model = new SubscriptionSummaryModel()
                {
                    SubscriptionList = Mapper.Map<List<SubscriptionDetailModel>>(subscriptionList.SubscriptionList),
                    Companies = companies,
                    Filter = companyFilter
                };

            }
            return View(model);
        }

        public bool SaveMarkup(SubscriptionDetailModel markup)
        {
            var markups = Mapper.Map<SubscriptionDetailModel, SubscriptionDetail>(markup);
            return _companyService.SaveMarkup(markups);
        }

        public bool CheckCompanyTable()
        {
            return _companyService.CheckCompanyTable();
        }
        public void addflag()
        {
            _companyService.addflag();
        }
        public ActionResult GetOrderDetail(CompanyOrderModels model)
        {
            //var order = Mapper.Map<PrivateLabelLite.Entities.CompanyOrder>(model);
            //// insert orders into database
            //if (order != null)
            //{

            //    var salesOrderIds = new List<string>();
            //    if (model.SalesOrderIdCSV != null && !string.IsNullOrEmpty(model.SalesOrderIdCSV))
            //    {
            //        salesOrderIds = order.SalesOrderIdCSV.Split(',').ToList();
            //        salesOrderIds.RemoveAll(x => String.IsNullOrEmpty(x));
            //        salesOrderIds = salesOrderIds.Distinct().ToList();
            //    }
            //    else
            //        salesOrderIds.Add(model.SalesOrderId);

            //    // get order details from api// order 
            //    List<OrderDetail> orderDetails = new List<OrderDetail>();
            //    order.SalesOrderIdCSV = "";
            //    foreach (var orderNumber in salesOrderIds)
            //    {
            //        try
            //        {
            //            var orderDetail = _partnerApi.GetOrderDetail(orderNumber).OrderInfo;
            //            _orderService.RemoveUnknownMSProductsAndUpdateMissingInfoFromDb(orderDetail);
            //            if (orderDetail != null)
            //            {
            //                orderDetails.Add(orderDetail);
            //                order.SalesOrderIdCSV = !string.IsNullOrEmpty(order.SalesOrderIdCSV) ? order.SalesOrderIdCSV + ", " + orderNumber : orderNumber;
            //            }
            //        }
            //        catch (Exception)
            //        {
            //            order.OrdersThatNotFound = !string.IsNullOrEmpty(order.OrdersThatNotFound) ? order.OrdersThatNotFound + ", " + orderNumber : orderNumber;
            //        }

            //    }
            //    _orderService.UpdateOrdersInfo(orderDetails);

            //}
            return null;
        }
    }
}