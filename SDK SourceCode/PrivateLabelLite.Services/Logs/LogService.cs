using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateLabelLite.Data.Repository.LogsRepo;
using PrivateLabelLite.Entities.Api;

namespace PrivateLabelLite.Services.Logs
{
    public class LogService : ILogService
    {
        ILogRepository _logRepo = new LogRepository();
        public void LogException(ApiErrorException apiError, ApiRequest apiRequest)
        {
            _logRepo.LogException(apiError, apiRequest);
        }
    }
}
