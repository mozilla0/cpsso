using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Product
{
    public class AddOn
    {
        public string sku { get; set; }
        public string SkuName { get; set; }
        public string Description { get; set; }
        public string ZeroValueSku { get; set; }
        public string QtyMin { get; set; }
        public string QtyMax { get; set; }
        public string VendorMapId { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string Article { get; set; }
    }
}
