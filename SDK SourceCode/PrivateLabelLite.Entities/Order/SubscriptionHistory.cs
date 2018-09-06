using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Order
{
    public class SubscriptionHistory
    {
        public string sku { get; set; }
        public string Message { get; set; }
        public int TotalSeats { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

}
