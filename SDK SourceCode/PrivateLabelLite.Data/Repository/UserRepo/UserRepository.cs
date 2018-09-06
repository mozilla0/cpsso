using PrivateLabelLite.Data.DataEntities;
using PrivateLabelLite.Entities;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Entities.EndUser;
using PrivateLabelLite.Entities.Subsciptions;
using PrivateLabelLite.Entities.User;
using PrivateLabelLite.Framework.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Data.Repository.UserRepo
{
    public class UserRepository : IUserRepository
    {
        #region Fields
        private readonly PrivateLabelLiteDataEntities _pllContext;
        #endregion

        #region Ctor
        public UserRepository(IDataContextFactory dataContextFactory)
        {
            this._pllContext = dataContextFactory.PLLDataContext();
        }
        #endregion

        public bool IsEndUserMappingExist(LoggedInUserInfo userInfo)
        {
            var userEmail = userInfo.Email.ToLower();
            var endUserMapping = _pllContext.Enduser.Where(x => x.Email == userEmail).FirstOrDefault();
            if (!String.IsNullOrEmpty(userEmail) && ((endUserMapping != null && endUserMapping.Email.ToLower() == userEmail) || (ConfigKeys.AllowedResellers ?? "").ToLower().Contains(userEmail)))
            {
                return true;
            }
            return false;
        }


        public bool IsUserAReseller(LoggedInUserInfo userInfo)
        {
            if (!String.IsNullOrEmpty(userInfo.Email) && (ConfigKeys.AllowedResellers ?? "").ToLower().Contains(userInfo.Email.ToLower()))
            {
                return true;
            }
            return false;
        }

        public Entities.User.EndUserDetail GetEndUserDetail(string email)
        {
            EndUserDetail endUser = null;
            var userDetail = _pllContext.Enduser.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
            if (userDetail != null)
            {
                endUser = new EndUserDetail()
                {
                    EnduserId = userDetail.EnduserId,
                    Name = userDetail.Name,
                    CompanyId = userDetail.CompanyId,
                    Created = userDetail.Created,
                    CreatedBy = userDetail.CreatedBy,
                    Email = userDetail.Email,
                    SAPEnduserId = userDetail.SAPEnduserId,
                };
                var company = _pllContext.Company.Where(x => x.CompanyId == endUser.CompanyId).FirstOrDefault();
                if (company != null)
                {
                    endUser.CompanyName = company.Name;
                }
            }
            return endUser;
        }

        public SubscriptionSummary GetUserSubscriptions(CompanyOrderFilter filter)
        {
            var subRes = new SubscriptionSummary()
            {
                SubscriptionList = new List<SubscriptionDetail>()

            };
            var data = _pllContext.procGetUserSubscriptions(filter.PageSize, filter.PageNo, filter.EndUserEmail, filter.EndUser,filter.ProductName);
            if(data!=null)
            {
                foreach (var subscription in data)
                {
                    var test = new SubscriptionDetail()

                    {
                        OrderNumber = subscription.OrderNumber,
                        MappingStatus = subscription.MappingStatus,
                        SKU = subscription.SKU,
                        Name = subscription.SkuName,
                        Quantity = subscription.Quantity,
                        AdditionalData = new AdditionalOrderLineData()
                        {
                            Domain = subscription.Domain
                        },
                        TotalRecords = subscription.MaxRows.ToInt(),
                        Company = subscription.Company,
                        SubscriptionId = subscription.SubscriptionId,
                        OrderStatus = subscription.Status,
                        LineStatus = subscription.LineStatus,
                        PONumber = subscription.PONumber,
                        OrderDate = subscription.OrderDate.ToDateTime(),
                        UnitPrice = subscription.UnitPrice,
                        SalesPrice = (Convert.ToDouble(subscription.UnitPrice)) +((subscription.MarkUpPercentage/100)* (Convert.ToDouble(subscription.UnitPrice))) ,
                        MarkUpPercentage = subscription.MarkUpPercentage,
                        SeatLimit = subscription.SeatLimit,
                        TaxStatus = subscription.TaxStatus,
                        SeatLimitStartTime = subscription.SeatLimitStartTime,
                        SeatLimitEndTime = subscription.SeatLimitEndTime,
                        SeatCounter = subscription.SeatCounter,
                        CurrencySymbol = subscription.CurrencySymbol




                    };
                    subRes.SubscriptionList.Add(test);


                }
            }

            return subRes;
           
        }


    }
}
