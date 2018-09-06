using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using PrivateLabelLite.Data;
using PrivateLabelLite.Data.DataEntities;
using PrivateLabelLite.Data.Repository.CompanyRepo;
using PrivateLabelLite.Data.Repository.OrderRepo;
using PrivateLabelLite.Data.Repository.ProductRepo;
using PrivateLabelLite.Data.Repository.SettingsRepo;
using PrivateLabelLite.Models;
using PrivateLabelLite.Services.Caching;
using PrivateLabelLite.Services.Company;
using PrivateLabelLite.Services.Order;
using PrivateLabelLite.Services.PartnerApi;
using PrivateLabelLite.Services.Product;
using PrivateLabelLite.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace SubscriptionWebJob
{
    class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            AppEngine.Instance.Container = container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType(typeof(IUserStore<ApplicationUser>), typeof(UserStore<ApplicationUser>));
            container.RegisterType<System.Data.Entity.DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<PrivateLabelLiteDataEntities>();

            container.RegisterType<IDataContextFactory, DataContextFactory>();

            container.RegisterType<ISettingsRepository, SettingsRepository>();
            container.RegisterType<ICompanyRepository, CompanyRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<ICompanyService, CompanyService>();
            container.RegisterType<ICacheService, CacheService>();
            container.RegisterType<ISettingsService, SettingsService>();
            container.RegisterType<IPartnerApi, PartnerApi>();


        }
    }
}
