using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Subsciptions
{
    public class SubscriptionDetailResult
    {
        //public List<SubscriptionDetail> Subscriptions { get; set; }
        public int totalPages { get; set; }
        public int totalRecords { get; set; }
        public List<Dictionary<Guid, SubscriptionDetail>> Subscriptions { get; set; }

    }
    public class SubscriptionSummary
    {
        public List<SubscriptionDetail> SubscriptionList { get; set; }
    }

    public class SubscriptionDetail
    {
        public string OrderNumber { get; set; }
        public List<string> OrderNumbers { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Article { get; set; }
        public string PaymentMethod { get; set; }
        public string EndCustomerName { get; set; }
        public string EndCustomerEmail { get; set; }
        public string Company { get; set; }
        public string OrderSource { get; set; }
        public string UnitPrice { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string LineStatus { get; set; }
        public List<SubscriptionHistory> SubscriptionHistory { get; set; }
        public AdditionalOrderLineData AdditionalData { get; set; }
        public int TotalRecords { get; set; }
        public Guid SubscriptionId { get; set; }
        public string MappingStatus { get; set; }
        public SubscriptionDetail()
        {
            this.OrderNumbers = new List<string>();
        }
        public string PONumber { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
        public double? MarkUpPercentage { get; set; }
        public double? SalesPrice { get; set; }
        public double? SeatLimit { get; set; }
        public string TaxStatus { get; set; }
        public DateTime? SeatLimitStartTime { get; set; }
        public DateTime? SeatLimitEndTime { get; set; }
        public int? SeatCounter { get; set; }
    }

    public class SubscriptionHistory
    {
        public string Sku { get; set; }
        public string Message { get; set; }
        public int TotalSeats { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
    public class AdditionalOrderLineData
    {
        public string Domain { get; set; }
        public string MsSubscriptionId { get; set; }
        public string MicrosoftId { get; set; }

    }
    public class Lines
    {
        public int Quantity { get; set; }
        public string SKU { get; set; }
    }

}
