using PrivateLabelLite.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Data.Repository.SettingsRepo
{
   public interface ISettingsRepository
    {
       List<AppSettings> GetAppSettings();
       string GetSiteTermsAndConditions();
    }
}
