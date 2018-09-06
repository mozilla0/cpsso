using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Product
{
    public class ProductDetail
    {
        public decimal Id { get; set; }
        public string Sku { get; set; }
        public string SkuName { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string Article { get; set; }
        public string VendorMapId { get; set; }
        public string ProductType { get; set; }
        public string QtyMin { get; set; }
        public string QtyMax { get; set; }
        public bool IsAddOn { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
