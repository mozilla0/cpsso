using PrivateLabelLite.Entities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities
{
  public class CompanyOrderFilter :  PagingInfo
    {
        public decimal CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string SalesOrderIds { get; set; }
        public string EndUser { get; set; }
        public string EndUserEmail { get; set; }
        public string Domain { get; set; }
        public string ResellerPO { get; set; }
        public string ProductName { get; set; }
        public string OrderNumber { get; set; }
        public bool EditProductStatus { get; set; }
    }
}
