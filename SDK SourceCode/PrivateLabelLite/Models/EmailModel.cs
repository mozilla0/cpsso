using Heroic.AutoMapper;
using PrivateLabelLite.Entities.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivateLabelLite.Models
{
    public class EmailModel : IMapTo<EmailDetails> , IMapFrom<EmailDetails>
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Icon { get; set; }
    }
}