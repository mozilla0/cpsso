using PrivateLabelLite.Data.Repository.UserRepo;
using PrivateLabelLite.Entities;
using PrivateLabelLite.Entities.Subsciptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Services.User
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IUserRepository _userRepository;
        #endregion

        #region Ctor
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        #endregion

        public bool IsEndUserMappingExist(Entities.User.LoggedInUserInfo userInfo)
        {
            return _userRepository.IsEndUserMappingExist(userInfo);
        }


        public bool IsUserAReseller(Entities.User.LoggedInUserInfo userInfo)
        {
            return _userRepository.IsUserAReseller(userInfo);
        }

        Entities.User.EndUserDetail IUserService.GetEndUserDetail(string email)
        {
            return _userRepository.GetEndUserDetail(email);
        }

        public SubscriptionSummary GetUserSubscriptions(CompanyOrderFilter filter)
        {
            return _userRepository.GetUserSubscriptions(filter);
        }
    }
}
