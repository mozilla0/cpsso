using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Api
{
    public class ApiRequest
    {
        public string Browser { get; set; }
        public string CurrentExecutionFilePath { get; set; }
        public string RequestType { get; set; }
        public string UserHostAddress { get; set; }
        public string UserHostName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
    public class Browser
    {
        public string Browsers{ get; set; }
    }
}
