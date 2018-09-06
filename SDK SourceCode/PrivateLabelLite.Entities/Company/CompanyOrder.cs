using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities
{
  public class CompanyOrder
    {
        public decimal RecordId { get; set; }
        public decimal CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string SalesOrderId  { get; set; }
        public string SalesOrderIdCSV { get; set; }
        public List<string> SalesOrderIdCSVs { get; set; }
        public int TotalRecords { get; set; }
        public string OrdersThatNotFound { get; set; }
    }
}
