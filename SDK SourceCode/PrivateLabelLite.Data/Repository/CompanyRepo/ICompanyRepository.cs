using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateLabelLite.Entities.EndUser;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Entities;
using PrivateLabelLite.Entities.User;
using PrivateLabelLite.Entities.Subsciptions;
namespace PrivateLabelLite.Data.Repository.CompanyRepo
{
    public interface ICompanyRepository
    {
        List<Customer> GetEndCustomers(EndCustomerFilter filter);
        Response AddEndUserMapping(EndCustomerFilter customer);
        List<CompanyDetail> GetCompanies(CompanyFilter filter);
        List<CompanyOrder> GetCompanyOrders(CompanyOrderFilter order);
        //List<SubscriptionDetail> GetSubscriptionDetail(CompanyOrderFilter filter);
        SubscriptionSummary GetSubscriptionDetail(CompanyOrderFilter filter);
        Response UpsertCompanyOrderMapping(CompanyOrder order);
        Response RemoveEndUserMapping(List<decimal> customerIds);
        Response DeleteCompanyOrderMapping(List<decimal> recordIds);
        Response SaveMapping(List<SubscriptionDetail> unMapped, List<SubscriptionDetail> mapped);
        Response UpdateCompanies(List<string> companies, LoggedInUserInfo userInfo);
        bool UpdateSubscriptionDetail(List<Dictionary<Guid, SubscriptionDetail>> subscriptions);
        bool SaveMarkup(SubscriptionDetail markup);
        bool CheckCompanyTable();
         void addflag();
    }
}
