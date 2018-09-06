using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PrivateLabelLite.Models;
using PrivateLabelLite.ActionFilter;
using PrivateLabelLite.Services.Settings;
using PrivateLabelLite.Entities.Common;

namespace PrivateLabelLite.Controllers
{
    [AuthorizeResellerAttribute]
    public class ConfigurationController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private SettingsService _settingsService;
        public ConfigurationController(SettingsService settingService)
        {
            _settingsService = settingService;
        }

        // GET: Settings
        [Route("Configuration")]
        public ActionResult Index()
        {
            return View(db.Config.ToList().OrderBy(x => x.Type).Reverse());
        }

        // GET: Settings/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Settings/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Key,Value,Type")] Config config)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Config.Add(config);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(config);
        //}

        // GET: Settings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = db.Config.Find(id);
            if (config == null)
            {
                return HttpNotFound();
            }

            return View(config);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Key,Value,Type")] Config config)
        {
            if (ModelState.IsValid)
            {
                var config_ = db.Config.Where(x => x.Id == config.Id).FirstOrDefault();
                if (config != null)
                {
                    config_.Value = config.Value;
                    db.SaveChanges();

                    //load configurationFromDataBase
                    if (_settingsService != null)
                    {
                        ConfigKeys.DbSettings = _settingsService.GetAppSettings();
                        ConfigKeys.LoadConfiguration();
                    }
                }
                return RedirectToAction("Index");
            }
            return View(config);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
