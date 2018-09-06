using PrivateLabelLite.Entities.EndUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivateLabelLite.Models
{
    public class EndCustomerModels
    {
        public EndCustomerFilter CustomerFilter { get; set; }
        public CustomersDetail CustomersInfo { get; set; }
    }
}