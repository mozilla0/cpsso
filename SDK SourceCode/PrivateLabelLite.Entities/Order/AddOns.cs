using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Order
{
    public class AddOns
    {
        public string sku { get; set; }
        public string SkuName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string AddOnStatus { get; set; }
        //public int MyProperty { get; set; }
        public AdditionalData AdditionalData { get; set; }
        // keep originalQuantity
        public string OriginalQuantity { get { return Quantity.ToString(); } }
    }
    public class AdditionalData
    {
        public List<SubscriptionHistory> SubscriptionHistory { get; set; }
    }
}
