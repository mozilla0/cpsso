using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Product
{
    public class VendorCatalogue
    {
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public List<Listing> Listings { get; set; }
    }
}
