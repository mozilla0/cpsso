using PrivateLabelLite.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrivateLabelLite.Helper;
namespace PrivateLabelLite.ActionFilter
{
    public class AuthorizeResellerAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var returnUrl = filterContext.HttpContext.Request.RawUrl;
            if (string.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.Email()) || !(System.Web.HttpContext.Current.User.Identity.IsAuthenticated && (ConfigKeys.AllowedResellers ?? "").ToLower().Contains(System.Web.HttpContext.Current.User.Identity.Email().ToLower())))
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/UnexpectedError.cshtml"
                };
            }
            base.OnAuthorization(filterContext);
        }
    }
}