using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Services.PartnerApi;
using PrivateLabelLite.Entities.EndUser;
using PrivateLabelLite.Models;
using PrivateLabelLite.Data.DataEntities;
using PrivateLabelLite.Framework.Helper;

namespace PrivateLabelLite.Controllers
{
    [Authorize]
    public class ResellerController : BaseController
    {
        private readonly IPartnerApi _partnerApi;
        public ResellerController(IPartnerApi partnerApi)
        {
            _partnerApi = partnerApi;
        }
        // GET: Reseller
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EndCustomer()
        {
            var model = new EndCustomerModels();
            var filter = new EndCustomerFilter()
            {
                Page = 1,
                RecordsPerPage = 10
            };
            model.CustomersInfo = _partnerApi.GetCustomersDetail(filter);
            model.CustomerFilter = filter;
            return View(model);
        }

        [HttpPost]
        public ActionResult GetCustomersInfo(EndCustomerFilter filter)
        {
            var customers = _partnerApi.GetCustomersDetail(filter);
            return Json(customers, JsonRequestBehavior.DenyGet);
        }


    }
}