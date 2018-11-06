using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateLabelLite.Framework.Helper;
using PrivateLabelLite.Entities.Settings;
namespace PrivateLabelLite.Entities.Common
{
    public class ConfigKeys
    {
        public static string TokenOAuth_URL;
        public static string client_id;
        public static string client_secret;
        public static string grant_type;
        public static string CustomerDetailURL;
        public static string SOIN;
        public static string CustomerSearchURL;
        public static string MicrosoftClientId;
        public static string MicrosoftClientSecret;
        public static string OrderSearchUrl;
        public static string OrderModifyUrl;
        public static string ModifyAddOnUrl;
        public static string OrderDetailUrl;
        public static string VendorListUrl;
        public static string AllowedResellers;
        public static int PageSize;
        public static string VendorCatalogSearchUrl;
        public static string ResellerName;
        public static string ResellerId;
        public static string ApplicationName;
        public static string NotificationEmails;
        public static string NotificationEmailFrom;
        public static int MSVendorCatalogIntervalMin;
        public static bool EnableSecureUrlScheme;
        public static int UpdateVendorCatalogIntervalHours;
        public static string AppUrl;
        public static string SubscriptionSearchUrl;
        public static string ProductPricingUrl;
        public static string WebPagesVersion;
        public static string GetAppSetting(string setting)
        {
            if (DbSettings != null)
            {

                var set = DbSettings.Where(x => (x.Name ?? "").ToLower().Contains(setting.ToLower())).FirstOrDefault();

                if (set != null)
                {

                    return set.Value;
                }

            }
            return ConfigurationManager.AppSettings.Get(setting);
        }
        public static List<AppSettings> DbSettings { get; set; }
        public static void LoadConfiguration()
        {
            TokenOAuth_URL = GetAppSetting("TokenOAuth");
            client_id = GetAppSetting("client_id");
            client_secret = GetAppSetting("client_secret");
            grant_type = GetAppSetting("grant_type");
            CustomerDetailURL = GetAppSetting("CustomerDetailURL");
            SOIN = GetAppSetting("SOIN");
            CustomerSearchURL = GetAppSetting("CustomerSearchURL");
            MicrosoftClientId = GetAppSetting("MicrosoftClientId");
            MicrosoftClientSecret = GetAppSetting("MicrosoftClientSecret");
            OrderSearchUrl = GetAppSetting("OrderSearchUrl");
            OrderModifyUrl = GetAppSetting("OrderModifyUrl");
            AllowedResellers = GetAppSetting("AllowedResellers");
            PageSize = GetAppSetting("PageSize").ToInt();
            OrderDetailUrl = GetAppSetting("OrderDetailUrl");
            ModifyAddOnUrl = GetAppSetting("ModifyAddOnUrl");
            VendorListUrl = GetAppSetting("VendorListUrl");
            VendorCatalogSearchUrl = GetAppSetting("VendorCatalogSearchUrl");
            ResellerName = GetAppSetting("ResellerName");
            ResellerId = GetAppSetting("ResellerId");
            ApplicationName = GetAppSetting("ApplicationName");
            NotificationEmails = GetAppSetting("NotificationEmails");
            NotificationEmailFrom = GetAppSetting("NotificationEmailFrom");
            MSVendorCatalogIntervalMin = GetAppSetting("MSVendorCatalogIntervalMin").ToInt();
            EnableSecureUrlScheme = GetAppSetting("EnableSecureUrlScheme").ToBoolean();
            UpdateVendorCatalogIntervalHours = GetAppSetting("UpdateVendorCatalogIntervalHours").ToInt();
            AppUrl = GetAppSetting("AppUrl");
            SubscriptionSearchUrl = GetAppSetting("SubscriptionSearchUrl");
            ProductPricingUrl = GetAppSetting("ProductPricingUrl");
            WebPagesVersion = GetAppSetting("webpages:Version");
        }
    }
}
