using PrivateLabelLite.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Services.Settings
{
   public interface ISettingsService
    {
       List<AppSettings> GetAppSettings();
       string GetSiteTermsAndConditions();
    }
}
