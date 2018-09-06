using PrivateLabelLite.Data.DataEntities;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Data.Repository.SettingsRepo
{
    public class SettingsRepository : ISettingsRepository
    {
        #region privateProperties
        private readonly PrivateLabelLiteDataEntities _pllContext;
        #endregion

        #region Ctor
        public SettingsRepository(IDataContextFactory dataContextFactory)
        {
            this._pllContext = dataContextFactory.PLLDataContext();
        }
        #endregion

        public List<Entities.Settings.AppSettings> GetAppSettings()
        {
            var settings = _pllContext.Configs.Select(x => new AppSettings()
                {
                    Id = x.Id,
                    Name = x.Key,
                    Value = x.Value,
                    Type = x.Type
                }).ToList();
            return settings;
        }
        public string GetSiteTermsAndConditions()
        {
            return _pllContext.SiteContent.Where(x => x.Key == "TermsAndConditions").Select(y=>y.Value).FirstOrDefault();
        }

        
    }
}
