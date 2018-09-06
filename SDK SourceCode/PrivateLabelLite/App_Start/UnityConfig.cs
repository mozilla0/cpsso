using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Linq;
using PrivateLabelLite.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using PrivateLabelLite.Controllers;
namespace PrivateLabelLite
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType(typeof(IUserStore<ApplicationUser>), typeof(UserStore<ApplicationUser>));
            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
           // container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterTypes(AllClasses.FromLoadedAssemblies().Where(x => x.FullName.StartsWith("PrivateLabelLite")),
               WithMappings.FromMatchingInterface,
               WithName.Default);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}