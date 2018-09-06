using PrivateLabelLite.Entities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Data.Repository.LogsRepo
{
    public interface ILogRepository
    {
        void LogException(ApiErrorException apiError, ApiRequest apiRequest);
    }
}
