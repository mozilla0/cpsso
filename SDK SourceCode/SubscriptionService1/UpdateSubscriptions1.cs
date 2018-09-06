using PrivateLabelLite.Services.PartnerApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using Microsoft.Practices.Unity;
using PrivateLabelLite.Services.Caching;
using PrivateLabelLite.Services.Settings;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Services.Company;
using PrivateLabelLite.Services.Order;
using PrivateLabelLite.Entities.Subsciptions;
using PrivateLabelLite.Entities.Order;

namespace SubscriptionService1
{
    public partial class UpdateSubscriptions1 : ServiceBase
    {
        private System.Timers.Timer timer1 = null;
        DateTime _scheduleTime;
        private IPartnerApi _partnerApi;
        private ICacheService _cacheService;
        private ISettingsService _SettingsService;
        private ICompanyService _companyService;
        private IOrderService _orderService;



        public UpdateSubscriptions1()
        {
            InitializeComponent();
            timer1 = new System.Timers.Timer();
            _scheduleTime = DateTime.Today.AddDays(0).AddHours(11).AddMinutes(55);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                DependencyResolver1.Initialise();
                _partnerApi = AppEngine.Instance.Container.Resolve<IPartnerApi>();
                _cacheService = AppEngine.Instance.Container.Resolve<ICacheService>();
                _SettingsService = AppEngine.Instance.Container.Resolve<ISettingsService>();
                _companyService = AppEngine.Instance.Container.Resolve<ICompanyService>();
                _orderService = AppEngine.Instance.Container.Resolve<IOrderService>();

                //Test if its a time in the past and protect setting _timer.Interval with a negative number which causes an error.
                timer1.Interval = _scheduleTime.Subtract(DateTime.Now).TotalSeconds * 1000;
                this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
                timer1.Enabled = true;
                Library1.WriteErrorLog("Subscription window service started.");
            }
            catch (Exception ex)
            {
                Library1.WriteErrorLog(ex.Message);
            }

        }

        public void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            // TODO: Insert monitoring activities here.
            if (_SettingsService != null && ConfigKeys.DbSettings == null)
            {
                ConfigKeys.DbSettings = _SettingsService.GetAppSettings();
                ConfigKeys.LoadConfiguration();
            }
            Library1.WriteErrorLog("CAlling Api");
            if (timer1.Interval != 24 * 60 * 60 * 1000)
            {
                timer1.Interval = 24 * 60 * 60 * 1000;
            }
            //Getting all subscriptions for reseller
            bool subResp = false;
            var subscriptions = _partnerApi.GetSubscriptiondetail().Subscriptions;
            Library1.WriteErrorLog("Saving subscription into Database now.");
            if (subscriptions != null)
            {
                subResp = _companyService.UpdateSubscriptionDetail(subscriptions);
            }

            Library1.WriteErrorLog("Extracting order numbers of Microsoft only.");
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

            Library1.WriteErrorLog("Making Order Detail call to get ResellerPO for Microsft Products only.");
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
            Library1.WriteErrorLog("Saving orders into Database now.");
            bool orderResp = _orderService.UpdateOrdersInfo(orderDetails);
            if (subResp == true && orderResp == true)
            {
                Library1.WriteErrorLog("SubscriptionSummaryDetail, OrderHeader, OrderLine tables has been successfully updated");
            }
            Library1.WriteErrorLog("Job has been done successfully.");
            // If tick for the first time, reset next run to every 24 hours
            if (timer1.Interval != 24 * 60 * 60 * 1000)
            {
                timer1.Interval = 24 * 60 * 60 * 1000;
            }
        }
        protected override void OnStop()
        {
            // timer1.Enabled = false;
            Library1.WriteErrorLog("Subscription window service stopped.");
        }
    }
}
