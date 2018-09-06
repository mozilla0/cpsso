using PrivateLabelLite.Data.DataEntities;
using PrivateLabelLite.Entities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Data.Repository.LogsRepo
{
    public class LogRepository : ILogRepository
    {
        PrivateLabelLiteDataEntities _pllContext = new PrivateLabelLiteDataEntities();
       public void LogException(ApiErrorException apiError, ApiRequest apiRequest)
        {
            _pllContext.procInsertIntoLogs(apiError.Message,apiError.ErrorCode, apiError.Result,apiError.Key, apiError.IsSuccess, apiError.IsValid,apiRequest.TimeStamp,apiRequest.Browser,
                apiRequest.CurrentExecutionFilePath, apiRequest.RequestType, apiRequest.UserHostAddress, apiRequest.UserHostName);
        }
    }
}
