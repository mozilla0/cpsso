using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Product
{
    public class VendorCatalogSearch
    {
        public List<string> VendorIds { get; set; }
        public int Page { get; set; }
    }
}
