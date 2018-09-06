using PrivateLabelLite.Entities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.EndUser
{
    public class Customer
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public int TotalRecords { get; set; }
    }
    public class CustomersDetail : PagingInfo
    {
        public List<Customer> EndCustomersDetails { get; set; }

    }
}
