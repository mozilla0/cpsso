using PrivateLabelLite.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Email
{
    public class EmailHelper
    {
        public static Response SendMail(EmailDetails email)
        {
            MailMessage ms = new MailMessage(ConfigKeys.NotificationEmailFrom.ToLower(), email.To, email.Subject, email.Body);
            ms.IsBodyHtml = true;
            SmtpClient smclient = new SmtpClient();
            smclient.EnableSsl = true;
            smclient.Send(ms);
            return new Response()
            {
                IsValid = true,
            };
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
