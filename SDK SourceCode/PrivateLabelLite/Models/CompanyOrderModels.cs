using Heroic.AutoMapper;
using PrivateLabelLite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivateLabelLite.Models
{
    public class CompanyOrderModels : IMapTo<CompanyOrder>,IMapFrom<CompanyOrder>
    {
        public decimal RecordId { get; set; }
        public decimal CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string SalesOrderId   { get; set; }
        public string SalesOrderIdCSV { get; set; }
        public List<string> SalesOrderIdCSVs { get; set; }

        public int TotalRecords { get; set; }
        public string OrdersThatNotFound { get; set; }
        public CompanyOrderModels()
        {
            this.SalesOrderIdCSVs = new List<string>();
        }
    }
}