using PrivateLabelLite.Entities;
using PrivateLabelLite.Entities.EndUser;
using PrivateLabelLite.Entities.Subsciptions;
using PrivateLabelLite.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Data.Repository.UserRepo
{
   public interface IUserRepository
    {
       bool IsEndUserMappingExist(LoggedInUserInfo userInfo);
       bool IsUserAReseller(LoggedInUserInfo userInfo);
       EndUserDetail GetEndUserDetail(string email);
       SubscriptionSummary GetUserSubscriptions(CompanyOrderFilter filter);
    }
}
