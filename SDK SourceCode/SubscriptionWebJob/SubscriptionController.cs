using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateLabelLite.Services.PartnerApi;
using PrivateLabelLite.Services.Caching;
using PrivateLabelLite.Services.Settings;
using PrivateLabelLite.Services.Company;
using PrivateLabelLite.Services.Order;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Entities.Order;
using PrivateLabelLite.Entities.Subsciptions;

namespace SubscriptionWebJob
{
    class SubscriptionController
    {
     
        private IPartnerApi _partnerApi;
        private ICacheService _cacheService;
        private ISettingsService _SettingsService;
        private ICompanyService _companyService;
        private IOrderService _orderService;


        public void GetSubscriptions()
        {
            
            Initialise();

            
            if (_SettingsService != null && ConfigKeys.DbSettings == null)
            {
                ConfigKeys.DbSettings = _SettingsService.GetAppSettings();
                ConfigKeys.LoadConfiguration();
            }
            
         
            //Getting all subscriptions for reseller
            bool subResp = false;
            var subscriptions = _partnerApi.GetSubscriptiondetail().Subscriptions;
            
            if (subscriptions != null)
            {
                subResp = _companyService.UpdateSubscriptionDetail(subscriptions);
            }


            //Extracting order numbers of Microsoft only.
            SubscriptionDetail ordernumbers = new SubscriptionDetail();
            for (int i = 0; i < subscriptions.Count; i++)
            {
                if ((subscriptions[i].Values.FirstOrDefault().VendorName == "Microsoft" && subscriptions[i].Values.FirstOrDefault().LineStatus != "cancelled"))
                {
                    var value = subscriptions[i].Values.FirstOrDefault().OrderNumber;

                    if (!ordernumbers.OrderNumbers.Contains(value))
                    {
                        ordernumbers.OrderNumbers.Add(value);
                    }

                }
            }


            //Making Order Detail call to get ResellerPO for Microsft Products only.
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var orderNumber in ordernumbers.OrderNumbers)
            {
                try
                {
                    string orderNum;
                    orderNum = Convert.ToString(orderNumber);
                    var orderDetail = _partnerApi.GetOrderDetail(orderNum).OrderInfo;
                    orderDetails.Add(orderDetail);
                }
                catch (Exception)
                {

                }

            }

            bool orderResp = _orderService.UpdateOrdersInfo(orderDetails);
        }
        private void Initialise()
        {
            Bootstrapper.Initialise();
            _partnerApi = AppEngine.Instance.Container.Resolve<IPartnerApi>();
            _cacheService = AppEngine.Instance.Container.Resolve<ICacheService>();
            _SettingsService = AppEngine.Instance.Container.Resolve<ISettingsService>();
            _companyService = AppEngine.Instance.Container.Resolve<ICompanyService>();
            _orderService = AppEngine.Instance.Container.Resolve<IOrderService>();

        }
    }
}
