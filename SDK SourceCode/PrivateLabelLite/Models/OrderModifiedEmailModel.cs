using Heroic.AutoMapper;
using PrivateLabelLite.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivateLabelLite.Models
{
    public class OrderModifiedEmailModel : IMapTo<OrderModifiedEmailDetail>, IMapFrom<OrderModifiedEmailDetail>
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CompanyName { get; set; }
        public string OrderNumber { get; set; }
        public int PreviousSeatCount { get; set; }
        public int CurrentSeatCount { get; set; }
        public string Sku { get; set; }
        public string SkuName { get; set; }
        public DateTime TimeAndDateChange { get; set; }
        public bool IsReseller { get; set; }
        public string EndUserEmail { get; set; }
        public string EndUserName { get; set; }
    }
}