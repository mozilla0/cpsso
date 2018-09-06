using PrivateLabelLite.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Entities.Email;
using PrivateLabelLite.Data.Repository.UserRepo;
namespace PrivateLabelLite.Services.Email
{
    public class EmailService : IEmailService
    {
        #region Fields
        private readonly IUserRepository _userRepository;
        #endregion

        #region Ctor
        public EmailService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        #endregion

        public void SendMailToResellerAndEndUser(OrderModifiedEmailDetail orderModifiedDetail)
        {

            if (orderModifiedDetail != null)
            {
                // get emails to send mail
                var emails = new List<string>();
                //emails.Add(orderModifiedDetail.CustomerEmail);
                emails.Add(orderModifiedDetail.EndUserEmail);
                emails.AddRange(ConfigKeys.NotificationEmails.Split(','));
                emails = emails.Select(x => x.ToLower()).Distinct().ToList();
                emails.RemoveAll(x => !EmailHelper.IsValidEmail(x));

                // send email async
                Task.Run(() => SendEmail(orderModifiedDetail.OrderNumber, orderModifiedDetail.CompanyName, orderModifiedDetail.EmailBody, emails));
            }
        }

        void SendEmail(string orderNumber, string companyName, string emailBody, List<string> emails)
        {
            EmailDetails emailDetails = new EmailDetails();
            foreach (var mailId in emails)
            {
                emailDetails.Body = emailBody.ToString();
                emailDetails.Subject = "Order Modified " + orderNumber + " " + companyName;
                emailDetails.To = mailId;
                EmailHelper.SendMail(emailDetails);
            }
        }
        public void NewUserNotification(EmailDetails emailDetails)
        {
            Task.Run(() => EmailHelper.SendMail(emailDetails));
        }
    }
}
