using PrivateLabelLite.Entities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Order
{
    public class OrderFilter : PagingInfo
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> skus { get; set; }
        public string PONumber { get; set; }
        public string EndUserEmail { get; set; }
        public List<string> OrderNumbers { get; set; }
        public string SalesOrderNo { get; set; }
        public string EndUser { get; set; }
        public string SkuName { get; set; }
        //public CompanyFilter Company { get; set; }
        public string CompanyName { get; set; }
    }
}
