using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Email
{
   public class EmailDetails
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Icon { get; set; }
    }
}
