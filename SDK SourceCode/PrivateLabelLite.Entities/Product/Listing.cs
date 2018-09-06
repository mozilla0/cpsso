using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Product
{
    public class Listing
    {
        public string ListingName { get; set; }
        public List<SKU> skus { get; set; }
    }
}
