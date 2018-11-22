using PrivateLabelLite.Data.DataEntities;
using PrivateLabelLite.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateLabelLite.Framework.Helper;
using PrivateLabelLite.Entities.User;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Entities;
using PrivateLabelLite.Entities.Product;

namespace PrivateLabelLite.Data.Repository.OrderRepo
{
    public class OrderRepository : IOrderRepository
    {
        #region Fields
        private readonly PrivateLabelLiteDataEntities _pllContext;
        #endregion

        #region Ctor
        public OrderRepository(IDataContextFactory dataContextFactory)
        {
            this._pllContext = dataContextFactory.PLLDataContext();
        }
        #endregion
        public bool IsOrderBelongsToUser(string orderNo, LoggedInUserInfo userInfo)
        {
            var userEmailLowercase = userInfo.Email.ToLower();

            if (string.IsNullOrEmpty(orderNo))
            {
                return false;
            }

            decimal? user_companyId = 0;
            var company_orderIds = "";

            if (!String.IsNullOrEmpty(userEmailLowercase))
            {
                // if user email id is in allowed resellers list in config treat it as reseller
                if (userInfo.IsUserAReseller)
                {
                    return true;
                }
                var endUser = _pllContext.Enduser.Where(x => (x.Email ?? "").ToLower() == userEmailLowercase).FirstOrDefault();
                if (endUser != null)
                {
                    user_companyId = endUser.CompanyId;
                }
                else return false;
                var companyOrder = _pllContext.SubscriptionSummaryDetail.Where(x => x.OrderNumber == orderNo).FirstOrDefault();
                company_orderIds = companyOrder != null ? companyOrder.OrderNumber : "";
            }
            return !String.IsNullOrEmpty(company_orderIds) ? true : false;
        }


        public OrderSearchResult GetOrders(OrderFilter filter, LoggedInUserInfo userInfo)
        {
            var orderSearchRes = new OrderSearchResult()
            {
                OrderSearch = new List<OrderDetail>()
            };

            var data = _pllContext.procXmlGetOrders(userInfo.IsUserAReseller, userInfo.Email, filter.SalesOrderNo, filter.EndUser, filter.SkuName, filter.PageSize, filter.PageNo, filter.CompanyName).ToList();
            if (data != null)
            {
                foreach (var order in data)
                {
                    orderSearchRes.TotalRecords = order.TotalRecords.ToStringValue();
                    orderSearchRes.OrderSearch.Add(
                            new OrderDetail()
                            {
                                OrderNumber = order.OrderNumber,
                                OrderDate = order.OrderDate.ToDateTime(),
                                Status = order.Status,
                                EndUserEmail = order.EndUserEmail,
                                EndUserName = order.EndUserName,
                                Total = order.TotalSalesPrice.ToDecimal(),
                                CurrencyCode = order.CurrencyCode,
                                CurrencySymbol = order.CurrencySymbol,
                                Lines = !String.IsNullOrEmpty(order.OrderLines) ?
                                        (XElement.Parse(order.OrderLines).Descendants("OrderLine").Select(x => new PrivateLabelLite.Entities.Order.OrderLine()
                                        {
                                            SKU = x.Attribute("Sku").AttributeValue(),
                                            SkuName = x.Attribute("SkuName").AttributeValue(),
                                            Quantity = x.Attribute("Quantity").AttributeValue(),
                                            UnitPrice = x.Attribute("UnitPrice").AttributeValue().ToDecimal(),
                                            ManufacturerPartNumber = x.Attribute("ManufacturerPartNumber").AttributeValue(),
                                            LineStatus = x.Attribute("LineStatus").AttributeValue(),
                                            CurrencySymbol = x.Attribute("CurrencySymbol").AttributeValue(),
                                            CurrencyCode = x.Attribute("CurrencyCode").AttributeValue(),
                                            AdditionalData = new AdditionalOrderLineData()
                                            {
                                                Domain = order.Domain
                                            }

                                        }).ToList()) : new List<PrivateLabelLite.Entities.Order.OrderLine>(),
                                Company = new Entities.CompanyFilter()
                                {
                                    CompanyId = order.CompanyId,
                                    CompanyName = order.CompanyName
                                }
                            });
                }
            }
            return orderSearchRes;
        }


        public bool UpdateOrdersInfo(List<OrderDetail> orders)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            foreach (var order in orders)
            {
                var orderJson = JsonConvert.SerializeObject(order, jsonSerializerSettings);
                var orderInfo = _pllContext.OrderHeader.Where(x => x.OrderNumber == order.OrderNumber).FirstOrDefault();
                if (orderInfo != null)
                {
                    orderInfo.Status = order.Status;
                    orderInfo.TotalSalesPrice = order.Total;
                    orderInfo.EndUserEmail = order.EndUserEmail;
                    orderInfo.EndUserName = order.EndUserName;
                    orderInfo.CurrencyCode = order.CurrencyCode;
                    orderInfo.CurrencySymbol = order.CurrencySymbol;
                    orderInfo.OrderDate = order.OrderDate;
                    orderInfo.OrderJson = orderJson;
                    orderInfo.PONumber = order.ResellerPoNumber;

                    foreach (var line in order.Lines)
                    {
                        var lineInfo = _pllContext.OrderLine.Where(x => (x.OrderNumber == order.OrderNumber) && x.Sku.Equals(line.SKU, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (lineInfo != null)
                        {
                            lineInfo.SkuName = line.SkuName;
                            lineInfo.LineStatus = line.LineStatus;
                            lineInfo.Quantity = line.Quantity.ToString();
                            lineInfo.CurrencyCode = line.CurrencyCode;
                            line.CurrencySymbol = line.CurrencySymbol;
                            lineInfo.UnitPrice = line.UnitPrice;
                            lineInfo.ManufacturerPartNumber = line.ManufacturerPartNumber;
                            // update domain
                            orderInfo.Domain = line.AdditionalData != null && !String.IsNullOrEmpty(line.AdditionalData.Domain) ? line.AdditionalData.Domain : orderInfo.Domain;
                        }
                    }
                }
                else
                {
                    _pllContext.OrderHeader.Add(
                        new OrderHeader()
                        {
                            OrderNumber = order.OrderNumber,
                            Status = order.Status,
                            EndUserName = order.EndUserName,
                            EndUserEmail = order.EndUserEmail,
                            OrderDate = order.OrderDate,
                            TotalSalesPrice = order.Total,
                            CurrencySymbol = order.CurrencySymbol,
                            CurrencyCode = order.CurrencyCode,
                            Domain = GetDomain(order.Lines),
                            PONumber = order.ResellerPoNumber,
                            OrderJson = orderJson
                        });
                    var lines = order.Lines.GroupBy(i => i.SKU).Select(a => a.FirstOrDefault()).ToList();
                    foreach (var line in lines)
                    {
                        _pllContext.OrderLine.Add(
                        new DataEntities.OrderLine()
                        {
                            OrderNumber = order.OrderNumber,
                            LineStatus = line.LineStatus,
                            ManufacturerPartNumber = line.ManufacturerPartNumber,
                            Quantity = line.Quantity.ToString(),
                            Sku = line.SKU,
                            SkuName = line.SkuName,
                            CurrencyCode = line.CurrencyCode,
                            CurrencySymbol = line.CurrencySymbol,
                            UnitPrice = line.UnitPrice
                        });
                    }
                }
            }
            // insert 
            _pllContext.SaveChanges();
            return true;
        }

        private string GetDomain(List<PrivateLabelLite.Entities.Order.OrderLine> lines)
        {
            var domain = "";
            if (lines != null && lines.Count > 0 && lines.FirstOrDefault().AdditionalData != null)
            {



                domain = lines.FirstOrDefault().AdditionalData.Domain;
            }
            return domain;
        }


        public OrderDetail GetOrderDetail(string orderNumber)
        {
            var order = _pllContext.OrderHeader.Where(x => x.OrderNumber == orderNumber).FirstOrDefault();
            var orderDetail = new OrderDetail();
            var company = new Company();
            if (order != null)
            {
                var companyDetail = _pllContext.CompanyOrder.Where(x => x.SalesOrderId == order.OrderNumber).FirstOrDefault();
                orderDetail = JsonConvert.DeserializeObject<OrderDetail>(order.OrderJson);
                if (orderDetail != null && companyDetail != null)
                {
                    company = _pllContext.Company.Where(x => x.CompanyId == companyDetail.CompanyId).FirstOrDefault();
                    orderDetail.Company = new CompanyFilter
                    {
                        CompanyId = companyDetail.CompanyId,
                        CompanyName = company.Name
                    };
                }
                else
                {
                    var subscriptionSummaryDetail = _pllContext.SubscriptionSummaryDetail.Where(x => x.OrderNumber == orderNumber).FirstOrDefault();
                    company = _pllContext.Company.Where(x => x.Name == subscriptionSummaryDetail.Company).FirstOrDefault();
                    orderDetail.Company = new CompanyFilter
                    {
                        CompanyId = company.CompanyId,
                        CompanyName = company.Name
                    };
                }
            }
            return orderDetail;
        }

        public bool checkDatabase()
        {
            var count = _pllContext.SubscriptionSummaryDetail.Count();
            if (count > 0)
            {
                return false;
            }
            else
                return true;
        }

        public List<ProductDetail> GetProductsFromSubscriptionSummary(string sku)
        {
            var subscriptionProducts = (from o in _pllContext.SubscriptionSummaryDetail.Where(x => x.SKU == sku)
                                        select new ProductDetail
                                        {
                                            Sku = o.SKU,
                                            VendorMapId = o.VendorId
                                        }).ToList();


            return subscriptionProducts;
        }
        public Entities.Order.OrderDetail GetUnitPrice(OrderDetail details)
        {
            OrderDetail orderDetail = new OrderDetail()
            {
                Lines = new List<Entities.Order.OrderLine>()
            };
            if (details != null)
            {
                foreach (var line in details.Lines)
                {
                    var detail = _pllContext.SubscriptionSummaryDetail.Where(x => x.SKU == line.SKU && x.OrderNumber == details.OrderNumber).FirstOrDefault();
                    if (detail != null)
                    {
                        //if (detail.SalesPrice == null) { detail.SalesPrice = 0; }
                        //if (detail.MarkUpPercentage == null) { detail.MarkUpPercentage = 0; }
                        //if (detail.SeatLimit == null) { detail.SeatLimit = 0; }
                        //if (detail.SeatCounter == null) { detail.SeatCounter = 0; }
                        orderDetail.Lines.Add(new Entities.Order.OrderLine()
                        {
                            UnitPrice = detail.UnitPrice.ToDecimal(),
                            SKU = detail.SKU,
                            SalesPrice = detail.SalesPrice,
                            MarkUpPercentage = detail.MarkUpPercentage,
                            SeatLimit = detail.SeatLimit,
                            TaxStatus = detail.TaxStatus,
                            SeatLimitStartTime = detail.SeatLimitStartTime.ToDateTime(),
                            SeatLimitEndTime = detail.SeatLimitEndTime.ToDateTime(),
                            SeatCounter = detail.SeatCounter,
                            CurrencySymbol = detail.CurrencySymbol,
                            LineStatus = detail.LineStatus

                        });
                    }

                }
            }
            return orderDetail;
        }

        public bool IsUserAuthorizeToIncreaseSeat(Entities.Order.OrderLine orderLine, string ordernumber, int originalQuantity)
        {
            int NewQuantity = Convert.ToInt32(orderLine.Quantity);
            orderLine.Quantity = Convert.ToString(NewQuantity - originalQuantity);
            var response = (from o in _pllContext.procIsUserAuthorizeToIncreaseSeat(orderLine.Quantity, orderLine.SKU, orderLine.SeatCounter, ordernumber, originalQuantity) select o).FirstOrDefault();

            //response = Convert.ToBoolean(response);
            return response.ToBoolean();


        }
        public bool UpdateSeatCountForDay(Entities.Order.OrderLine orderLine, string ordernumber, int originalQuantity)
        {
            int NewQuantity = Convert.ToInt32(orderLine.Quantity);
            float Quantity = (NewQuantity - originalQuantity);
            var orders = _pllContext.SubscriptionSummaryDetail.Where(x => x.OrderNumber == ordernumber && x.SKU == orderLine.SKU);
            foreach (var order in orders)
            {
                order.TemporarySeatLimit = (order.TemporarySeatLimit - Quantity);
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
    }
}
