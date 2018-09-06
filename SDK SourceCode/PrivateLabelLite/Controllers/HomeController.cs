using PrivateLabelLite.Models;
using PrivateLabelLite.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateLabelLite.Controllers
{
    public class HomeController : BaseController
    {
        private ISettingsService _settingsService;
        public HomeController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public ActionResult TermsAndConditions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetSiteTermsAndConditions()
        {
            var termsAndConditions = _settingsService.GetSiteTermsAndConditions();
            return Json(termsAndConditions, JsonRequestBehavior.DenyGet);
        }
    }
}