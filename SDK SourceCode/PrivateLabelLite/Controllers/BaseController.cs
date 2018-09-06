using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PrivateLabelLite.ActionFilter;
using PrivateLabelLite.Entities.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PrivateLabelLite.Helper;
using PrivateLabelLite.Entities.Common;


namespace PrivateLabelLite.Controllers
{
    [Authorize]
    [ApiExceptionFilter]
    public class BaseController : Controller
    {

        protected new ActionResult Json(object data, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            if (Request.RequestType == WebRequestMethods.Http.Get && behavior == JsonRequestBehavior.DenyGet)
                throw new InvalidOperationException("GET is no permitted for this request");

            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(data, jsonSerializerSettings),
                ContentType = "application/json"
            };
            return jsonResult;
        }
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public LoggedInUserInfo GetLoggedInUserInfo()
        {
            LoggedInUserInfo user = new LoggedInUserInfo();
            user.Email = User.Identity.Email();
            user.UserName = User.Identity.Name;
            user.IsUserAReseller = IsUserAReseller();
            return user;
        }

        private bool IsUserAReseller()
        {
            if (!String.IsNullOrEmpty(User.Identity.Email()) && (ConfigKeys.AllowedResellers ?? "").ToLower().Contains(User.Identity.Email().ToLower()))
            {
                return true;
            }
            return false;
        }
     
        public ViewResult ErrorHandler(string message)
        {
            return View();
        }
    }
}