using PrivateLabelLite.Entities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Product
{
    public class VendorCatalogSearchResult
    {
        public ProductsByVendor Products { get; set; }
    }
    public class ProductsByVendor : PagingInfo
    {
        public int TotalProducts { get; set; }
        public int TotalPages { get; set; }
        public List<VendorCatalogue> Vendors { get; set; }
    }
}
