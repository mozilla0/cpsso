using PrivateLabelLite.Entities.Email;
using PrivateLabelLite.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Services.Email
{
    public interface IEmailService
    {
        void SendMailToResellerAndEndUser(OrderModifiedEmailDetail orderModifiedDetail);
        void NewUserNotification(EmailDetails emailDetails);

    }
}
