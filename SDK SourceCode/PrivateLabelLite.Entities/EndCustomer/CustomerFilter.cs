using PrivateLabelLite.Entities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.EndUser
{
    public class EndCustomerFilter : PagingInfo
    {
        public decimal Id { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public decimal CompanyId { get; set; }
        public string CustomerId { get; set; }


    }
}
