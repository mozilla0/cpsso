using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateLabelLite.Entities;
using PrivateLabelLite.Entities.EndUser;
using PrivateLabelLite.Entities.Order;
using PrivateLabelLite.Entities.Vendor;
using PrivateLabelLite.Entities.Product;
using PrivateLabelLite.Entities.Subsciptions;

namespace PrivateLabelLite.Services.PartnerApi
{
    public interface IPartnerApi
    {
        T Post<T>(string uri, List<KeyValuePair<string, string>> headers, object postObj);
        T Get<T>(string uri, List<KeyValuePair<string, string>> headers = null);
        AccessToken GenerateToken(string url = null, List<KeyValuePair<string, string>> headers = null, List<KeyValuePair<string, string>> bodyItems = null);
        CustomersDetail GetCustomersDetail(EndCustomerFilter filter);
        OrderSearchResult GetOrders(OrderFilter filter);
        ModifyOrderResult ModifyOrder(ModifyOrder modifiedOrder);
        OrderDetailResult GetOrderDetail(string orderNumber);
        SubscriptionDetailResult GetSubscriptiondetail();
        ModifyOrderAddonsResult ModifyOrderAddOns(ModifyOrderAddons modifiedAddOns);
        VendorList GetVendors();
        VendorCatalogSearchResult GetProductsByVendor(VendorCatalogSearch filter);
        VendorCatalogSearchResult GetAllProductsByVendor(VendorCatalogSearch filter);
        Entities.Product.Catalogue GetMicrosoftVendorCatalogue(); 
        CustomersDetail GetAllCustomers();
        Lines GetProductPriceInfo(List<Lines> lines);
    }
}
