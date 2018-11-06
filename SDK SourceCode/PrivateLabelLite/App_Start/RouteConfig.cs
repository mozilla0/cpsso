using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PrivateLabelLite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
              name: "configuration",
              url: "Configuration",
              defaults: new { controller = "Configuration", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "getOrderDetail", 
                url: "order/OrderDetail/{OrderNumber}/{CompanyId}",
                defaults: new { controller = "Order", action = "OrderDetail", OrderNumber = UrlParameter.Optional, CompanyId = UrlParameter.Optional });

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Order", action = "Subscriptions", id = UrlParameter.Optional }
           );
            
        }
    }
}
