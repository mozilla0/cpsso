using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Order
{
    public class ModifyOrderAddonsResult
    {
        public OrderAddOnsDetails OrderAddOnsDetails { get; set; }
    }
    public class OrderAddOnsDetails
    {
        public string OrderNumber { get; set; }
        public string BaseSubscription { get; set; }
        public List<AddOnResult> AddOns { get; set; }
    }
    public class AddOnResult
    {
        public string Result { get; set; }
        public string AddOnSku { get; set; }
        public string Message { get; set; }
    }
}
