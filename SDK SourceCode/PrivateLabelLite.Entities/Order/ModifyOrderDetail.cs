using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Order
{
   public class ModifyOrder
    {
        public List<ModifyOrderDetail> ModifyOrders { get; set; }
    }
    public class ModifyOrderDetail
    {
        public string EndUserEmail { get; set; }
        public string EndUserName { get; set; }
        public string Action { get; set; }
        public string OrderNumber { get; set; }
        public string sku { get; set; }
        public string SkuName { get; set; }
        public int OriginalQuantity { get; set; }
        public int NewQuantity { get; set; }
        public MetaData MetaData { get; set; }
    }
}
