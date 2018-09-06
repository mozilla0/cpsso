using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Order
{
    public class ModifyOrderAddons
    {
        public ModifiedAddonDetail ModifyAddons { get; set; }
    }
    public class ModifiedAddonDetail
    {
        public string OrderNumber { get; set; }
        public string EndUserEmail { get; set; }
        public string EndUserName { get; set; }
        public string BaseSubscription { get; set; }
        public AddOnModify AddOn { get; set; }
        public List<AddOnModify> AddOns { get; set; }
        public MetaData MetaData { get; set; }
    }
    public class AddOnModify
    {
        public string Action { get; set; }
        public string AddOnSku { get; set; }
        public string SkuName { get; set; }
        public string OriginalQuantity { get; set; }
        public string NewQuantity { get; set; }
    }
    public class MetaData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsEndCustomer { get; set; }
    }
}
