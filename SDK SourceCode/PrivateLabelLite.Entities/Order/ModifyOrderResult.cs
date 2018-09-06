using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Order
{
  public class ModifyOrderResult
    {
      public List<ModifyOrderDetails> ModifyOrdersDetails { get; set; }
    }
    public class ModifyOrderDetails
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string sku { get; set; }
        public string OrderNumber { get; set; }
    }
}
