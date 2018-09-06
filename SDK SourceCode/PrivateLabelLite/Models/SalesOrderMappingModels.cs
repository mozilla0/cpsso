using PrivateLabelLite.Data.DataEntities;
using PrivateLabelLite.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace PrivateLabelLite.Models
{
    public class SalesOrderMappingModels
    {
        public CompanyOrderModels CompanyOrder { get; set; }
        public List<CompanyDetail> Companies { get; set; }
        public List<CompanyOrderModels> CompanyOrders { get; set; }
        public CompanyOrderFilter Filter { get; set; }
    }
}