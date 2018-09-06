using PrivateLabelLite.Entities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;



namespace PrivateLabelLite.Services.Logs
{
    public interface ILogService 
    {
        void LogException(ApiErrorException apiError, ApiRequest apiRequest);
    }
}
