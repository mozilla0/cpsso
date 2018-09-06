using PrivateLabelLite.Data.Repository.SettingsRepo;
using PrivateLabelLite.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        #region privateProperties
        private readonly ISettingsRepository _settingsRepo;
        #endregion

        #region Ctor
        public SettingsService(ISettingsRepository settingsRepo)
        {
            this._settingsRepo = settingsRepo;
        }
        #endregion
        public List<AppSettings> GetAppSettings()
        {
            return _settingsRepo.GetAppSettings();
        }


        public string GetSiteTermsAndConditions()
        {
            return _settingsRepo.GetSiteTermsAndConditions();
        }
    }
}
