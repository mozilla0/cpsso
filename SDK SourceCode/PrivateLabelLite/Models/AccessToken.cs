using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivateLabelLite.Models
{
    public class AccessToken
    {
        public string Access_Token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}