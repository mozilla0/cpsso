using Heroic.AutoMapper;
using PrivateLabelLite.Data.DataEntities;
using PrivateLabelLite.Entities;
using PrivateLabelLite.Entities.EndUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivateLabelLite.Models
{
    public class CompanyEndUserMappingModels
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string EndUserId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public List<EndUserInfoModel> EndUsers { get; set; }
        public EndCustomerFilter Filter { get; set; }
        public decimal CompanyId { get; set; }
        public List<CompanyFilter> Companies { get; set; }
    }
    public class EndUserInfoModel : IMapFrom<Customer>
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public int TotalRecords { get; set; }
    }
}