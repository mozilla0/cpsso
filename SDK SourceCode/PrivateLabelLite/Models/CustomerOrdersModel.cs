using PrivateLabelLite.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Heroic.AutoMapper;
using PrivateLabelLite.Entities;
namespace PrivateLabelLite.Models
{
    public class CustomerOrdersModel
    {
        public string TotalRecords { get; set; }
        public List<OrderInfoModel> Orders { get; set; }
        public OrderFilter Filter { get; set; }
        public List<CompanyFilter> Companies { get; set; }
    }
    public class OrderInfoModel : IMapFrom<OrderDetail>
    {
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public string PONumber { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string EndUserName { get; set; }
        public string EndUserEmail { get; set; }
        public string OrderSource { get; set; }
        public decimal Total { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyCode { get; set; }
        public List<OrderLine> Lines { get; set; }
        public CompanyFilter Company { get; set; }
    }
}