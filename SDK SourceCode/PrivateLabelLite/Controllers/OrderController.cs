using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PrivateLabelLite.Services.PartnerApi;
using PrivateLabelLite.Entities.Order;
using PrivateLabelLite.Models;
using AutoMapper;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Framework.Helper;
using PrivateLabelLite.Services.Order;
using PrivateLabelLite.Services.User;
using PrivateLabelLite.Entities.User;
using System.Web.Routing;
using PrivateLabelLite.Services.Email;
using PrivateLabelLite.Services.Company;
using PrivateLabelLite.Entities;
using System.Text;

namespace PrivateLabelLite.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        #region Fields
        private readonly IPartnerApi _partnerApi;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly ICompanyService _companyService;

        #endregion

        #region Ctor
        public OrderController(IPartnerApi partnerApi, IOrderService orderService, IUserService userService, IEmailService emailService, ICompanyService companyService)
        {
            this._partnerApi = partnerApi;
            this._orderService = orderService;
            this._userService = userService;
            this._emailService = emailService;
            this._companyService = companyService;
        }
        #endregion

        // GET: Order       
        [HttpGet]
        public ActionResult Index()
        {
            var loggedInuserInfo = GetLoggedInUserInfo();
            if (!_userService.IsEndUserMappingExist(loggedInuserInfo))
            {
                return View("~/Views/Shared/UnexpectedError");
            }

            CustomerOrdersModel model = null;
            var orderFilter = new OrderFilter()
            {
                Page = 1,
                RecordsPerPage = ConfigKeys.PageSize,

            };
            var orderSearch = _orderService.GetOrders(orderFilter, loggedInuserInfo);
            if (orderSearch != null)
            {
                orderFilter.TotalRecords = orderSearch.TotalRecords;
                model = new CustomerOrdersModel()
                {
                    Orders = Mapper.Map<List<OrderInfoModel>>(orderSearch.OrderSearch),
                    Filter = orderFilter
                };

            }
            return View(model);
            //return RedirectToAction("Company", "Home");
        }
        public ActionResult Subscriptions(string id)
        {
            string allCompany = id;
            if (id != null)
            {

                var companyFilter = new CompanyOrderFilter()
                {
                    Page = 1,
                    RecordsPerPage = ConfigKeys.PageSize,
                    CompanyName = id
                };
                var companies = _companyService.GetCompanies(new CompanyFilter());
                companies.Insert(0, new CompanyDetail { CompanyName = "ALL" });

                companyFilter.CompanyName = id;
                var subscriptionList = _companyService.GetSubscriptionDetail(companyFilter);

                SubscriptionSummaryModel model = new SubscriptionSummaryModel()
                {
                    SubscriptionList = Mapper.Map<List<SubscriptionDetailModel>>(subscriptionList.SubscriptionList),
                    Companies = companies,
                    Filter = companyFilter
                };
                model.Filter.CompanyName = allCompany;
                return View(model);
            }

            else
            {
                SubscriptionSummaryModel model = null;
                var companies = _companyService.GetCompanies(new CompanyFilter());
                companies.Insert(0, new CompanyDetail { CompanyName = "ALL" });
                var companyFilter = new CompanyOrderFilter()
                {
                    Page = 1,
                    RecordsPerPage = ConfigKeys.PageSize
                };
                model = new SubscriptionSummaryModel()
                {
                    Companies = companies,
                    Filter = companyFilter
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult GetOrders(OrderFilter filter)
        {
            TempData["isRedirected"] = true;
            return RedirectToAction("GetOrders", "OrderAsync", new RouteValueDictionary(filter));
        }


        [HttpGet]
        public ActionResult OrderDetail(string id, string companyName)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }
            else if (!_userService.IsEndUserMappingExist(GetLoggedInUserInfo()) || !_orderService.DoesOrderBelongsToUser(id, GetLoggedInUserInfo()))
            {
                return View("UserConfigurationError");
            }
            OrderDetailModel model = new OrderDetailModel()
            {
                OrderNo = id,
                OrderDetail = _orderService.GetOrderDetail(id),
                ModifyOrder = new ModifyOrder(),
                ModifyAddOnsDetail = new ModifyOrderAddons(),
                CompanyName = companyName
            };



            return View(model);
        }

        [HttpPost]
        public ActionResult RefereshOrderDetail(string id)
        {
            return RedirectToAction("RefereshOrderDetail", "OrderAsync", new { id });
        }

        // modify order subsciption
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyOrderDetail(ModifyOrderDetail modifiedOrders)
        {
            ModifyOrder modifiedOrder = new ModifyOrder();
            modifiedOrder.ModifyOrders = new List<Entities.Order.ModifyOrderDetail>();
            modifiedOrder.ModifyOrders.Add(new Entities.Order.ModifyOrderDetail()
            {
                EndUserEmail = modifiedOrders.EndUserEmail,
                EndUserName = modifiedOrders.EndUserName,
                Action = modifiedOrders.Action,
                OrderNumber = modifiedOrders.OrderNumber,
                sku = modifiedOrders.sku,
                SkuName = modifiedOrders.SkuName,
                OriginalQuantity = modifiedOrders.OriginalQuantity,
                NewQuantity = modifiedOrders.NewQuantity,
                MetaData = new MetaData() { FirstName = modifiedOrders.MetaData.FirstName, LastName = modifiedOrders.MetaData.LastName, IsEndCustomer = modifiedOrders.MetaData.IsEndCustomer }

            });

            if (modifiedOrder == null || modifiedOrder.ModifyOrders == null || !_orderService.DoesOrderBelongsToUser(modifiedOrder.ModifyOrders.FirstOrDefault().OrderNumber, GetLoggedInUserInfo()))
            {
                return null;
            }
            var orderModify = modifiedOrder.ModifyOrders.FirstOrDefault();
            var result = _partnerApi.ModifyOrder(modifiedOrder);
            var resp = new Response();
            if (result != null && result.ModifyOrdersDetails != null)
            {
                resp.IsValid = result.ModifyOrdersDetails.FirstOrDefault().Status.ToLower() == "success" ? true : false;
                resp.Message = result.ModifyOrdersDetails.FirstOrDefault().Message;
                // send notification email on success
                if (resp.IsValid)
                {
                    //send mail
                    SendOrderModificationEmail(orderModify.OrderNumber, orderModify.sku, orderModify.SkuName, orderModify.EndUserEmail,
                        orderModify.EndUserName, orderModify.OriginalQuantity, orderModify.NewQuantity);
                }
            }
            return Json(resp, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyOrderAddOns(ModifyOrderAddons addOnss)
        {
            ModifyOrderAddons addOns = new ModifyOrderAddons();
            addOns.ModifyAddons = new ModifiedAddonDetail()
            {
                OrderNumber = addOnss.ModifyAddons.OrderNumber,
                EndUserEmail = addOnss.ModifyAddons.EndUserEmail,
                EndUserName = addOnss.ModifyAddons.EndUserName,
                BaseSubscription = addOnss.ModifyAddons.BaseSubscription,
                MetaData = new MetaData() { FirstName = addOnss.ModifyAddons.MetaData.FirstName, LastName = addOnss.ModifyAddons.MetaData.LastName, IsEndCustomer = addOnss.ModifyAddons.MetaData.IsEndCustomer },

            };

            List<AddOnModify> associatedAddOns = new List<AddOnModify>();
            associatedAddOns.Add(addOnss.ModifyAddons.AddOn);
            addOns.ModifyAddons.AddOns = associatedAddOns;

            LoggedInUserInfo loggedInUser = GetLoggedInUserInfo();
            if (addOns == null || addOns.ModifyAddons == null || !_orderService.DoesOrderBelongsToUser(addOns.ModifyAddons.OrderNumber, loggedInUser))
            {
                return null;
            }



            //List<AddOnModify> mappedAddOns = new List<AddOnModify>();
            //mappedAddOns.Add(new AddOnModify()
            //{
            //    Action = addOns.ModifyAddons.AddOn.Action,
            //    AddOnSku = addOns.ModifyAddons.AddOn.AddOnSku,
            //    SkuName = addOns.ModifyAddons.AddOn.SkuName,
            //    OriginalQuantity = addOns.ModifyAddons.AddOn.OriginalQuantity,
            //    NewQuantity = addOns.ModifyAddons.AddOn.NewQuantity
            //});

            var addOn = addOns.ModifyAddons.AddOns.FirstOrDefault();
            var result = _partnerApi.ModifyOrderAddOns(addOns);
            var resp = new Response();
            if (result != null && result.OrderAddOnsDetails != null)
            {
                resp.IsValid = result.OrderAddOnsDetails.AddOns.FirstOrDefault().Result.ToLower() == "success" ? true : false;
                resp.Message = result.OrderAddOnsDetails.AddOns.FirstOrDefault().Message;

                // send email on success                
                if (addOn != null && resp.IsValid)
                {
                    // send mail
                    SendOrderModificationEmail(addOns.ModifyAddons.OrderNumber, addOn.AddOnSku, addOn.SkuName, addOns.ModifyAddons.EndUserEmail, addOns.ModifyAddons.EndUserName
                        , addOn.OriginalQuantity.ToInt(), addOn.NewQuantity.ToInt());
                }

            }
            return Json(resp, JsonRequestBehavior.DenyGet);

        }

        private void SendOrderModificationEmail(string orderNumber, string sku, string skuName, string customerEmail,
            string customerName, int previousSeatCount, int currentSeatCount)
        {
            var loggedInuserInfo = GetLoggedInUserInfo();
            // get user
            var endUser = _userService.GetEndUserDetail(customerEmail);
            var companyName = endUser != null ? endUser.CompanyName : "";
            var orderModifiedEmailDetail = new OrderModifiedEmailDetail()
            {
                OrderNumber = orderNumber,
                Sku = sku,
                SkuName = skuName,
                CustomerEmail = customerEmail,
                CustomerName = customerName,
                PreviousSeatCount = previousSeatCount,
                CurrentSeatCount = currentSeatCount,
                CompanyName = companyName,
                TimeAndDateChange = DateTime.Now,
                EndUserEmail = loggedInuserInfo.Email,
                EndUserName = loggedInuserInfo.UserName
            };

            OrderModifiedEmailModel emailModel = Mapper.Map<OrderModifiedEmailModel>(orderModifiedEmailDetail);

            orderModifiedEmailDetail.EmailBody = RenderRazorViewToString("Email", emailModel);

            // send email
            _emailService.SendMailToResellerAndEndUser(orderModifiedEmailDetail);
        }

        [HttpPost]
        private OrderDetail UpdateOrder(string id)
        {
            var orderDetail = _partnerApi.GetOrderDetail(id).OrderInfo;
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            if (orderDetail != null)
            {
                orderDetails.Add(orderDetail);
                _orderService.RemoveUnknownMSProductsAndUpdateMissingInfoFromDb(orderDetail);
                _orderService.UpdateOrdersInfo(orderDetails);
            }
            return orderDetail;
        }

        [HttpPost]
        public ActionResult UpdateProducts()
        {
            Session["UpdateProducts"] = "UpdateProducts";
            return RedirectToAction("UpdateProducts", "OrderAsync");
        }

        public ActionResult GetUserSubscriptions(CompanyOrderFilter filter)
        {
            var companies = _companyService.GetCompanies(new CompanyFilter());
            var companyFilter = new CompanyOrderFilter()
            {
                Page = 1,
                RecordsPerPage = ConfigKeys.PageSize
            };
            var loggedInuserInfo = GetLoggedInUserInfo();
            if (!_userService.IsEndUserMappingExist(loggedInuserInfo))
            {
                Response res = new Response();
                res.Message = "End user mapping does not exist";
                res.IsValid = false;
                return Json(res, JsonRequestBehavior.AllowGet);

            }
            filter.EndUserEmail = loggedInuserInfo.Email;
            filter.EndUser = loggedInuserInfo.UserName;
            var subscriptionList = _userService.GetUserSubscriptions(filter);

            SubscriptionSummaryModel model = new SubscriptionSummaryModel()
            {
                SubscriptionList = Mapper.Map<List<SubscriptionDetailModel>>(subscriptionList.SubscriptionList),
                Companies = companies,
                Filter = companyFilter

            };
           
                return Json(model, JsonRequestBehavior.AllowGet);
           
        }

        public void DownloadSubscriptionHistory(string id)
        {
            var data = _orderService.GetOrderDetail(id);
            List<SubscriptionHistory> subscriptionHistory = new List<SubscriptionHistory>();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("SKU" + "," + "Description" + "," + "TotalSeats" + "," + "Total Seats Requested/ Status Change" + "," + "Created By" + "," + "CreatedOn" + "\r\n");
            foreach (var line in data.Lines)
            {
                if (line.AdditionalData != null)
                {
                    var subscriptions = line.AdditionalData.SubscriptionHistory.ToList();
                    var sortedSubscriptions = subscriptions.OrderByDescending(c => c.CreatedOn).ToList();
                    var additionalSkuName = line.SkuName;

                    foreach (var x in sortedSubscriptions)
                    {
                        var subcriptionModel = new SubscriptionHistory
                        {
                            CreatedBy = x.CreatedBy,
                            CreatedOn = x.CreatedOn,
                            sku = x.sku,
                            Message = x.Message,
                            TotalSeats = x.TotalSeats
                        };
                        stringBuilder.AppendLine(subcriptionModel.sku + "," + additionalSkuName + "," + subcriptionModel.TotalSeats + "," + subcriptionModel.Message + "," + subcriptionModel.CreatedBy + "," + subcriptionModel.CreatedOn);
                    }
                }
                //var results = (from b in data.Lines from a in b.AddOns select a.AdditionalData).ToList();


                if (line.AddOns != null)
                {
                    foreach (var x in line.AddOns)
                    {
                        var addonSkuName = x.SkuName;
                        if (x.AdditionalData != null)


                        {
                            var addonSubscriptions = x.AdditionalData.SubscriptionHistory.ToList();
                            var sortedAddonSubscriptions = addonSubscriptions.OrderByDescending(c => c.CreatedOn).ToList();
                            foreach (var val in sortedAddonSubscriptions)
                            {
                                var subcriptionModel = new SubscriptionHistory
                                {
                                    CreatedBy = val.CreatedBy,
                                    CreatedOn = val.CreatedOn,
                                    sku = val.sku,
                                    Message = val.Message,
                                    TotalSeats = val.TotalSeats
                                };
                                stringBuilder.AppendLine(subcriptionModel.sku + "," + addonSkuName + "," + subcriptionModel.TotalSeats + "," + subcriptionModel.Message + "," + subcriptionModel.CreatedBy + "," + subcriptionModel.CreatedOn);
                            }
                        }

                    }
                }

                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("");
            }

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "History/" + id + ".csv"));
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write(stringBuilder);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();

        }

        public ActionResult History(string lines)
        {
            return null;
        }
        public ActionResult GetUnitPrice(OrderDetail details)
        {
            var prices = _orderService.GetUnitPrice(details);
            return Json(prices.Lines,JsonRequestBehavior.DenyGet);

        }
        public bool IsUserAuthorizeToIncreaseSeat(OrderLine orderLine,string ordernumber,int originalQuantity)
        {
            return _orderService.IsUserAuthorizeToIncreaseSeat(orderLine,ordernumber, originalQuantity);
        }

        public bool UpdateSeatCountForDay(OrderLine orderLine, string ordernumber, int originalQuantity)
        {
            return _orderService.UpdateSeatCountForDay(orderLine, ordernumber, originalQuantity);
        }

        public ActionResult NotAuthorized()
        {
            return null;
        }
    }
}