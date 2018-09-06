using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.User
{
    public class LoggedInUserInfo
    {
        public string Email { get; set; }
        public bool IsUserAReseller { get; set; }
        public string UserName { get; set; }

    }
}
