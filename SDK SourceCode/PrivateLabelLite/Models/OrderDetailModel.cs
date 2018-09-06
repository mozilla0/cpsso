using PrivateLabelLite.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivateLabelLite.Models
{
    public class OrderDetailModel
    {
        public string OrderNo { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public ModifyOrder ModifyOrder { get; set; }
        public ModifyOrderAddons ModifyAddOnsDetail { get; set; }
        public string CompanyName { get; set; }
    }
}