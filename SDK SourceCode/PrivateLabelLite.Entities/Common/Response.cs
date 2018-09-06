using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Common
{
    public class Response
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
        public bool AdditionalFlag { get; set; }
    }
}
