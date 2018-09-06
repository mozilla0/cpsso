using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.User
{
    public class EndUserDetail
    {
        public decimal EnduserId { get; set; }
        public Nullable<System.Guid> SAPEnduserId { get; set; }
        public Nullable<decimal> CompanyId { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
    }
}
