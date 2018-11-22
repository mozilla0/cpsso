using PrivateLabelLite.Data.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateLabelLite.Framework.Helper;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Entities.EndUser;
using System.Xml.Linq;
using PrivateLabelLite.Entities.User;
using PrivateLabelLite.Entities.Subsciptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data.Entity.Validation;
using System.IO;
using System.Data.Entity.Infrastructure;
using PrivateLabelLite.Entities;

namespace PrivateLabelLite.Data.Repository.CompanyRepo
{
    public class CompanyRepository : ICompanyRepository
    {
        #region privateProperties
        private readonly PrivateLabelLiteDataEntities _pllContext;
        #endregion

        #region Ctor
        public CompanyRepository(IDataContextFactory dataContextFactory)
        {
            this._pllContext = dataContextFactory.PLLDataContext();
        }
        #endregion

        public List<Entities.EndUser.Customer> GetEndCustomers(Entities.EndUser.EndCustomerFilter filter)
        {
            return _pllContext.procGetUsers(filter.Id, filter.Name, filter.CompanyName, filter.PageSize, filter.PageNo, filter.OrderBy, filter.CompanyId)
                 .Select(x => new Entities.EndUser.Customer()
                 {
                     CompanyName = x.CompanyName,
                     Name = x.EndUserName,
                     Id = x.EnduserId.ToStringValue(),
                     Email = x.Email,
                     TotalRecords = x.TotalRecords.ToInt()
                 }).ToList();

        }

        public Response AddEndUserMapping(EndCustomerFilter customer)
        {
            var resp = new Response()
            {
                IsValid = true
            };
            // get user infor
            var endUser = _pllContext.Enduser.Where(x => x.EnduserId == customer.Id).FirstOrDefault();
            var company = _pllContext.Company.Where(x => x.Name == customer.CompanyName).FirstOrDefault();
            // check user if exist and it's email is to update
            if (endUser != null && endUser.Email.ToLower() != customer.Email.ToLower())
            {
                if (_pllContext.Enduser.Any(x => x.Email.ToLower() == customer.Email.ToLower()))
                    resp.IsValid = false;
            }
            // check if user email exist
            else if (endUser == null && _pllContext.Enduser.Any(x => x.Email.ToLower() == customer.Email.ToLower() && x.CompanyId == company.CompanyId))
            {
                resp.IsValid = false;
            }

            if (!resp.IsValid)
            {
                resp.Message = "User email already exist.";
                return resp;
            }

            if (endUser != null)
            {
                endUser.Email = customer.Email;
                endUser.Name = customer.Name;
                endUser.CompanyId = company.CompanyId;
                _pllContext.SaveChanges();
            }
            else
            {
                _pllContext.Enduser.Add(new Enduser() { CompanyId = company.CompanyId, Email = customer.Email, Created = DateTime.Now, CreatedBy = customer.Email, Name = customer.Name });
                _pllContext.SaveChanges();
            }

            resp.IsValid = true;
            resp.Message = "User mapped successfully.";
            return resp;
        }


        public List<Entities.CompanyDetail> GetCompanies(Entities.CompanyFilter filter)
        {
            return _pllContext.Company
                 .Select(x => new Entities.CompanyDetail()
                 {
                     CompanyName = x.Name,
                     CompanyId = x.CompanyId
                 }).OrderBy(x => x.CompanyName)
                 .ToList();
        }


        public List<Entities.CompanyOrder> GetCompanyOrders(Entities.CompanyOrderFilter order)
        {
            return _pllContext.procGetCompanySalesOrders(order.CompanyId, order.EndUser, order.SalesOrderIds, order.PageSize, order.PageNo)
                .Select(x => new Entities.CompanyOrder() { CompanyName = x.CompanyName, SalesOrderId = x.SalesOrderId, CompanyId = Convert.ToDecimal(x.CompanyId), TotalRecords = x.TotalRecords.ToInt(), RecordId = x.RecordId }).ToList();
        }

        public SubscriptionSummary GetSubscriptionDetail(CompanyOrderFilter filter)
        {
            var subRes = new SubscriptionSummary()
            {
                SubscriptionList = new List<SubscriptionDetail>()

            };


            var data = _pllContext.procGetSubscriptionSummary(filter.PageSize, filter.PageNo, filter.CompanyName, filter.Domain,
                filter.ProductName, filter.ResellerPO, filter.OrderNumber, filter.EditProductStatus);

            if (data != null)
            {

                foreach (var subscription in data)
                {
                    var test = new SubscriptionDetail()

                    {
                        OrderNumber = subscription.OrderNumber,
                        MappingStatus = subscription.MappingStatus,
                        SKU = subscription.SKU,
                        Name = subscription.SkuName,
                        Quantity = subscription.Quantity.ToString(),
                        AdditionalData = new AdditionalOrderLineData()
                        {
                            Domain = subscription.Domain
                        },
                        TotalRecords = subscription.MaxRows.ToInt(),
                        Company = subscription.Company,
                        SubscriptionId = subscription.SubscriptionId,
                        OrderStatus = subscription.Status,
                        LineStatus = subscription.LineStatus,
                        PONumber = subscription.PONumber,
                        OrderDate = subscription.OrderDate,
                        UnitPrice = subscription.UnitPrice,
                        SalesPrice = (Convert.ToDouble(subscription.UnitPrice)) + ((subscription.MarkUpPercentage / 100) * (Convert.ToDouble(subscription.UnitPrice))),
                        MarkUpPercentage = subscription.MarkUpPercentage,
                        SeatLimit = subscription.SeatLimit,
                        TaxStatus = subscription.TaxStatus,
                        SeatLimitStartTime = subscription.SeatLimitStartTime,
                        SeatLimitEndTime = subscription.SeatLimitEndTime,
                        SeatCounter = subscription.SeatCounter,
                        CurrencySymbol = subscription.CurrencySymbol

                    };
                    subRes.SubscriptionList.Add(test);


                }
            }
            return subRes;
        }
        public Response UpsertCompanyOrderMapping(Entities.CompanyOrder order)
        {
            var response = new Response()
            {
                IsValid = true,
                Message = "",
            };
            var companyOrder = _pllContext.CompanyOrder.Where(x => x.RecordId == order.RecordId).FirstOrDefault();
            if (companyOrder != null)
            {
                if (_pllContext.CompanyOrder.Where(x => x.SalesOrderId == order.SalesOrderId && x.RecordId != companyOrder.RecordId).FirstOrDefault() == null)
                {
                    companyOrder.CompanyId = order.CompanyId;
                    companyOrder.SalesOrderId = order.SalesOrderId;
                    response.Message = order.SalesOrderId;
                }
                else
                {
                    response.IsValid = false;
                    response.ErrorMessage = order.SalesOrderId;
                }
            }
            else
            {
                List<string> soIds = !String.IsNullOrEmpty(order.SalesOrderIdCSV) ? order.SalesOrderIdCSV.Split(',').ToList() : null;
                foreach (var so in soIds)
                {
                    var orderNo = so.Trim();
                    if (!_pllContext.CompanyOrder.Any(x => (x.SalesOrderId ?? "").Trim() == orderNo) && !String.IsNullOrEmpty(orderNo))
                    {
                        _pllContext.CompanyOrder.Add(new DataEntities.CompanyOrder() { CompanyId = order.CompanyId, SalesOrderId = orderNo, Created = DateTime.Now });
                        response.Message = !string.IsNullOrEmpty(response.Message) ? response.Message + ", " + orderNo : orderNo;
                    }
                    else
                    {
                        response.ErrorMessage = !string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage + ", " + orderNo : orderNo;
                    }
                }
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    response.IsValid = false;
                }
            }
            _pllContext.SaveChanges();

            return response;
        }


        public Response RemoveEndUserMapping(List<decimal> customerIds)
        {
            var xml = new XElement("CustomerIds");
            foreach (var customerId in customerIds)
            {
                xml.Add(new XElement("CustomerId", customerId));
            }
            var user = _pllContext.procXmlRemoveEndUser(xml.ToString());
            return new Response()
            {
                IsValid = true,
                Message = "Enduser mapping deleted successfully."
            };

        }

        public Response UpdateCompanies(List<string> companies, LoggedInUserInfo userInfo)
        {
            var response = new Response();
            var xml = new XElement("Companies");

            foreach (var company in companies)
            {
                xml.Add(new XElement("Company", company));
            }
            _pllContext.procXmlInsertCompanies(xml.ToString(), userInfo.Email);
            response.IsValid = true;
            return response;
        }
        public Response DeleteCompanyOrderMapping(List<decimal> recordIds)
        {
            var xml = new XElement("RecordIds");
            foreach (var recordId in recordIds)
            {
                xml.Add(new XElement("RecordId", recordId));
            }
            var user = _pllContext.procXmlDeleteCompanyOrderMapping(xml.ToString());
            //var companyOrder = _pllContext.CompanyOrder.Where(x => x.SalesOrderId == order.SalesOrderId && x.RecordId == order.RecordId);
            //if (companyOrder != null)
            //{
            //    _pllContext.CompanyOrder.RemoveRange(companyOrder);
            //}
            //// remove order details
            //var orderLines = _pllContext.OrderLine.Where(x => x.OrderNumber == order.SalesOrderId);
            //if (orderLines != null)
            //{
            //    _pllContext.OrderLine.RemoveRange(orderLines);
            //}
            //var orderHeader = _pllContext.OrderHeader.Where(x => x.OrderNumber == order.SalesOrderId);
            //if (orderHeader != null)
            //{
            //    _pllContext.OrderHeader.RemoveRange(orderHeader);
            //}

            //_pllContext.SaveChanges();
            return new Response()
            {
                IsValid = true,
                Message = "Company order mapping removed successfully."
            };
        }

        private string GetDomain(AdditionalOrderLineData AdditionalData)
        {
            var domain = "";
            if (AdditionalData != null)
            {
                domain = AdditionalData.Domain;
            }

            return domain;
        }
        private string GetMsSubscriptionId(AdditionalOrderLineData AdditionalData)
        {
            var MsSubscriptionId = "";
            if (AdditionalData.Domain != null)
            {
                MsSubscriptionId = AdditionalData.MsSubscriptionId;
            }
            return MsSubscriptionId;
        }

        private string GetMicrosoftId(AdditionalOrderLineData AdditionalData)
        {
            var MicrosoftId = "";
            if (AdditionalData.MicrosoftId != null)
            {
                MicrosoftId = AdditionalData.MicrosoftId;
            }
            return MicrosoftId;

        }

        public Response SaveMapping(List<SubscriptionDetail> unMapped, List<SubscriptionDetail> mapped)
        {
            if (unMapped.Count > 0)
            {
                foreach (var subscriptions in unMapped)
                {
                    var subscription = _pllContext.SubscriptionSummaryDetail.Where(x => x.OrderNumber == subscriptions.OrderNumber && x.SKU == subscriptions.SKU).FirstOrDefault();
                    if (subscription != null)
                    {
                        subscription.MappingStatus = "NOT MAPPED";
                    }
                }
            }
            if (mapped.Count > 0)
            {
                foreach (var subscriptions in mapped)
                {
                    var subscription = _pllContext.SubscriptionSummaryDetail.Where(x => x.OrderNumber == subscriptions.OrderNumber && x.SKU == subscriptions.SKU).FirstOrDefault();
                    if (subscription != null)
                    {
                        subscription.MappingStatus = "MAPPED";
                    }
                }
            }
            try
            {
                _pllContext.SaveChanges();
                return new Response()
                {
                    IsValid = true,
                    Message = "Mapping Saved Successfully."
                };
            }
            catch (Exception e)
            {
                return new Response()
                {
                    IsValid = false,
                    Message = "Mapping Failed."
                };
            }
        }

        public bool UpdateSubscriptionDetail(List<Dictionary<Guid, SubscriptionDetail>> subscriptions)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var subscriptionList = new List<SubscriptionDetail>();
            var subscriptionId = new List<Guid>();


            for (int i = 0; i < subscriptions.Count; i++)
            {
                if (subscriptions[i].Values.FirstOrDefault().VendorName == "Microsoft" && subscriptions[i].Values.FirstOrDefault().LineStatus != "cancelled")
                {
                    var value = subscriptions[i].Values.FirstOrDefault();
                    value.SubscriptionId = subscriptions[i].Keys.FirstOrDefault();
                    //System.Diagnostics.Debug.WriteLine(value.SubscriptionId);
                    var isExist = subscriptionList.Where(j => j.SubscriptionId == value.SubscriptionId).FirstOrDefault();
                    if (isExist == null)
                    {
                        subscriptionList.Add(value);
                    }
                }
            }

            foreach (var subscription in subscriptionList)
            {
                var historyJson = JsonConvert.SerializeObject(subscription.SubscriptionHistory, jsonSerializerSettings);
                var subscriptionInfo = _pllContext.SubscriptionSummaryDetail.Where(x => x.OrderNumber == subscription.OrderNumber && x.SubscriptionId == subscription.SubscriptionId && x.SKU == subscription.SKU).FirstOrDefault();
                var skuName = subscription.Name;
                skuName = skuName.Length >= 50 ? subscription.Name.Substring(0, 50) : subscription.Name;
                if (subscriptionInfo != null)
                {
                    subscriptionInfo.VendorId = subscription.VendorId;
                    subscriptionInfo.VendorName = subscription.VendorName;
                    subscriptionInfo.SKU = subscription.SKU;
                    subscriptionInfo.SkuName = skuName;
                    subscriptionInfo.Quantity = subscription.Quantity;
                    subscriptionInfo.Article = subscription.Article;
                    subscriptionInfo.PaymentMethod = subscription.PaymentMethod;
                    subscriptionInfo.EndCustomerEmail = subscription.EndCustomerEmail;
                    subscriptionInfo.EndCustomerName = subscription.EndCustomerName;
                    subscriptionInfo.Company = subscription.Company;
                    subscriptionInfo.OrderSource = subscription.OrderSource;
                    subscriptionInfo.UnitPrice = subscription.UnitPrice;
                    subscriptionInfo.CurrencyCode = subscription.CurrencyCode;
                    subscriptionInfo.CurrencySymbol = subscription.CurrencySymbol;
                    subscriptionInfo.CreatedDate = subscription.CreatedDate;
                    subscriptionInfo.UpdatedDate = subscription.UpdatedDate;
                    subscriptionInfo.LineStatus = subscription.LineStatus;
                    subscriptionInfo.Domain = subscription.AdditionalData != null ? GetDomain(subscription.AdditionalData) : string.Empty;
                    subscriptionInfo.MsSubscriptionId = subscription.AdditionalData != null ? GetMsSubscriptionId(subscription.AdditionalData) : string.Empty;
                    subscriptionInfo.MicrosoftId = subscription.AdditionalData != null ? GetMicrosoftId(subscription.AdditionalData) : string.Empty;
                    subscriptionInfo.SubscriptionHistoryJson = historyJson;
                }
                else
                {
                    _pllContext.SubscriptionSummaryDetail.Add(new SubscriptionSummaryDetail()
                    {
                        OrderNumber = subscription.OrderNumber != null ? subscription.OrderNumber : "",
                        VendorId = subscription.VendorId != null ? subscription.VendorId : "",
                        VendorName = subscription.VendorName,
                        SKU = subscription.SKU,
                        SkuName = skuName,
                        Quantity = subscription.Quantity,
                        Article = subscription.Article,
                        PaymentMethod = subscription.PaymentMethod,
                        EndCustomerEmail = subscription.EndCustomerEmail,
                        EndCustomerName = subscription.EndCustomerName,
                        Company = subscription.Company,
                        OrderSource = subscription.OrderSource,
                        UnitPrice = subscription.UnitPrice,
                        CurrencyCode = subscription.CurrencyCode,
                        CurrencySymbol = subscription.CurrencySymbol,
                        CreatedDate = subscription.CreatedDate,
                        UpdatedDate = subscription.UpdatedDate,
                        LineStatus = subscription.LineStatus,
                        Domain = subscription.AdditionalData != null ? GetDomain(subscription.AdditionalData) : string.Empty,
                        MsSubscriptionId = subscription.AdditionalData != null ? GetMsSubscriptionId(subscription.AdditionalData) : string.Empty,
                        MicrosoftId = subscription.AdditionalData != null ? GetMicrosoftId(subscription.AdditionalData) : string.Empty,
                        SubscriptionHistoryJson = historyJson != null ? historyJson : string.Empty,
                        SubscriptionId = subscription.SubscriptionId,
                        MappingStatus = "NOT MAPPED"
                    });
                }
            }
            _pllContext.SaveChanges();
            return true;
            //catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            //{
            //    Exception raise = dbEx;

            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        string entityTypeName = validationErrors.Entry.Entity.GetType().Name;

            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            string message = string.Format("{0}:{1}:{2}:{3}",
            //                validationErrors.Entry.Entity.ToString(),
            //                validationError.ErrorMessage, entityTypeName, validationError.PropertyName);
            //            // raise a new exception nesting
            //            // the current instance as InnerException
            //            raise = new InvalidOperationException(message, raise);
            //        }
            //    }
            //    throw raise;
            //}
        }

        public bool SaveMarkup(SubscriptionDetail markup)
        {
            if (String.IsNullOrEmpty(markup.OrderNumber) && String.IsNullOrEmpty(markup.SKU))
            {
                if (markup.Company == "ALL")
                {
                    var orders = _pllContext.SubscriptionSummaryDetail;
                    foreach (var order in orders)
                    {
                        if (markup.MarkUpPercentage != null)
                        {
                            order.MarkUpPercentage = markup.MarkUpPercentage;
                            double unitPrice = Convert.ToDouble(order.UnitPrice);
                            order.SalesPrice = unitPrice + ((markup.MarkUpPercentage / 100) * (unitPrice));
                        }
                        if (markup.SeatLimit != null)
                        {
                            order.SeatLimit = markup.SeatLimit;
                            order.TemporarySeatLimit = markup.SeatLimit;
                        }
                        if (markup.TaxStatus != null)
                        {
                            order.TaxStatus = markup.TaxStatus;
                        }
                        order.SeatCounter = 0;
                    };
                }
                else
                {
                    var orders = _pllContext.SubscriptionSummaryDetail.Where(x => x.Company == markup.Company);
                    foreach (var order in orders)
                    {
                        if (markup.MarkUpPercentage != null)
                        {
                            order.MarkUpPercentage = markup.MarkUpPercentage;
                            double unitPrice = Convert.ToDouble(order.UnitPrice);
                            order.SalesPrice = unitPrice + ((markup.MarkUpPercentage / 100) * (unitPrice));
                        }
                        if (markup.SeatLimit != null)
                        {
                            order.SeatLimit = markup.SeatLimit;
                            order.TemporarySeatLimit = markup.SeatLimit;
                        }
                        if (markup.TaxStatus != null)
                        {
                            order.TaxStatus = markup.TaxStatus;
                        }
                        order.SeatCounter = 0;
                    };
                }

            }
            else
            {
                var orders = _pllContext.SubscriptionSummaryDetail.Where(x => x.OrderNumber == markup.OrderNumber && x.SKU == markup.SKU && x.SubscriptionId == markup.SubscriptionId);
                foreach (var order in orders)
                {
                    if (markup.MarkUpPercentage != null)
                    {
                        order.MarkUpPercentage = markup.MarkUpPercentage;
                        double unitPrice = Convert.ToDouble(order.UnitPrice);
                        order.SalesPrice = unitPrice + ((markup.MarkUpPercentage / 100) * (unitPrice));
                    }
                    if (markup.SeatLimit != null)
                    {
                        order.SeatLimit = markup.SeatLimit;
                        order.TemporarySeatLimit = markup.SeatLimit;
                    }
                    if (markup.TaxStatus != null)
                    {
                        order.TaxStatus = markup.TaxStatus;
                    }
                    order.SeatCounter = 0;
                };
            }
            try
            {
                _pllContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool RemoveMarkup(SubscriptionDetail markup)
        {
            if (String.IsNullOrEmpty(markup.OrderNumber) && String.IsNullOrEmpty(markup.SKU))
            {
                if (markup.Company == "ALL")
                {
                    var orders = _pllContext.SubscriptionSummaryDetail;
                    foreach (var order in orders)
                    {
                        order.MarkUpPercentage = null;
                        order.SalesPrice = null;
                        order.SeatLimit = null;
                        order.TemporarySeatLimit = null;
                        order.TaxStatus = null;
                        order.SeatCounter = null;
                    };
                }
                else
                {
                    var orders = _pllContext.SubscriptionSummaryDetail.Where(x => x.Company == markup.Company);
                    foreach (var order in orders)
                    {
                        order.MarkUpPercentage = null;
                        order.SalesPrice = null;
                        order.SeatLimit = null;
                        order.TemporarySeatLimit = null;
                        order.TaxStatus = null;
                        order.SeatCounter = null;
                    };
                }

            }
            else
            {
                var orders = _pllContext.SubscriptionSummaryDetail.Where(x => x.OrderNumber == markup.OrderNumber && x.SKU == markup.SKU && x.SubscriptionId == markup.SubscriptionId);
                foreach (var order in orders)
                {
                    order.MarkUpPercentage = null;
                    order.SalesPrice = null;
                    order.SeatLimit = null;
                    order.TemporarySeatLimit = null;
                    order.TaxStatus = null;
                    order.SeatCounter = null;
                };
            }
            try
            {
                _pllContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool CheckCompanyTable()
        {
            var count = _pllContext.Company.Count();
            if (count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public void addflag()
        {
            _pllContext.SiteContent.Add(new SiteContent()
            {
                Id = 2,
                Key = "service",
                Value = "Subscription"
            });
        }
    }
}
