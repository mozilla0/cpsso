using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Services.PartnerApi;
using PrivateLabelLite.Services.Product;
using PrivateLabelLite.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PrivateLabelLite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static bool IsInitialized = false;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
            System.Web.Helpers.AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
            System.Web.Helpers.AntiForgeryConfig.UniqueClaimTypeIdentifier = System.Security.Claims.ClaimTypes.NameIdentifier;
            AutoMapperConfig.Configure();
            InitAppSettings();
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                InitAppSettings();
            }
        }
        void InitAppSettings()
        {
            if (!IsInitialized)
            {
                LoadConfigSettings();
                InitializeProducts();
            }
            IsInitialized = true; // so it will run only one time
        }

        void LoadConfigSettings()
        {
            var _settingsService = DependencyResolver.Current.GetService<SettingsService>();
            if (_settingsService != null && ConfigKeys.DbSettings == null)
            {
                ConfigKeys.DbSettings = _settingsService.GetAppSettings();
                ConfigKeys.LoadConfiguration();
            }
        }
        void InitializeProducts()
        {
            var _productService = DependencyResolver.Current.GetService<IProductService>();
            if (_productService != null && !IsInitialized)
            {
                _productService.UpdateProducts(ProductUpdateType.Initialize);
            }
        }
    }
}
