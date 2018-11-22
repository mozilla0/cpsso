using PrivateLabelLite.Entities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Order
{
    public class OrderSearchResult : PagingInfo
    {
        public List<OrderDetail> OrderSearch { get; set; }
    }
    public class OrderDetailResult 
    {
        public OrderDetail OrderInfo { get; set; }
    }
    public class OrdersDetailsResult
    {
        public List<OrderDetailResult> OrdersInfoResult { get; set; }
    }
    public class OrderDetail
    {
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public string ResellerPoNumber { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string EndUserName { get; set; }
        public string EndUserEmail { get; set; } 
        public string OrderSource { get; set; }
        public decimal Total { get; set; }
        public string CurrencySymbol { get; set; } 
        public string CurrencyCode { get; set; }
        public List<OrderLine> Lines { get; set; }
        public CompanyFilter Company { get; set; }
        public string AddOnSkuName { get; set; }
    }
    public class OrderLine
    {
        public string SKU { get; set; }
        public string SkuName { get; set; }
        public decimal UnitPrice { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string Quantity { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyCode { get; set; }
        public string LineStatus { get; set; }
        public AdditionalOrderLineData AdditionalData { get; set; }
        public List<AddOns> AddOns { get; set; }
        // keep originalQuantity
        public string OriginalQuantity { get { return Quantity; } }
        public double? MarkUpPercentage { get; set; }
        public double? SalesPrice { get; set; }
        public double? SeatLimit { get; set; }
        public string TaxStatus { get; set; }
        public DateTime? SeatLimitStartTime { get; set; }
        public DateTime? SeatLimitEndTime { get; set; }
        public int? SeatCounter { get; set; }

    }
    public class AdditionalOrderLineData
    {
        public string Domain { get; set; }
        public string AdministratorEmail { get; set; }
        public List<string> Bundles { get; set; }
        public List<SubscriptionHistory> SubscriptionHistory { get; set; }
    }
    


}
