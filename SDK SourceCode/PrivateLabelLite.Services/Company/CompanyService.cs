using PrivateLabelLite.Data.Repository.CompanyRepo;
using PrivateLabelLite.Entities;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Entities.EndUser;
using PrivateLabelLite.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateLabelLite.Entities.Subsciptions;


namespace PrivateLabelLite.Services.Company
{
   public class CompanyService :  ICompanyService
    {
       #region privateProperties
        private readonly ICompanyRepository _companyRepo;
        #endregion

        #region Ctor
        public CompanyService(ICompanyRepository companyRepo)
        {
            this._companyRepo = companyRepo;
        }
        #endregion
        public List<Customer> GetEndCustomers(EndCustomerFilter filter)
        {
            return _companyRepo.GetEndCustomers(filter);
        }

        public Response AddEndUserMapping(EndCustomerFilter customer)
        {
            return _companyRepo.AddEndUserMapping(customer);
        }

        public List<CompanyDetail> GetCompanies(CompanyFilter filter)
        {
            return _companyRepo.GetCompanies(filter);
        }

        public List<CompanyOrder> GetCompanyOrders(CompanyOrderFilter order)
        {
            return _companyRepo.GetCompanyOrders(order);
        }

        //public List<SubscriptionDetail> GetSubscriptionDetail(CompanyOrderFilter filter)
        //{
        //    return _companyRepo.GetSubscriptionDetail(filter);
        //}
        public SubscriptionSummary GetSubscriptionDetail(CompanyOrderFilter filter)
        {
            string allCompany = filter.CompanyName;
            if (filter.CompanyName == "ALL")
            {
                filter.CompanyName = "";
            }
          
            return _companyRepo.GetSubscriptionDetail(filter);
        }
        public Response UpsertCompanyOrderMapping(CompanyOrder order)
        {
            return _companyRepo.UpsertCompanyOrderMapping(order);
        }

        public Response RemoveEndUserMapping(List<decimal> customerIds)
        {
            return _companyRepo.RemoveEndUserMapping(customerIds);
        }

        public Response DeleteCompanyOrderMapping(List<decimal> recordIds)
        {
            return _companyRepo.DeleteCompanyOrderMapping(recordIds);
        }


        public Response UpdateCompanies(List<string> companies, LoggedInUserInfo userInfo)
        {
            return _companyRepo.UpdateCompanies(companies,userInfo);
        }
        public Response SaveMapping(List<SubscriptionDetail> unMapped, List<SubscriptionDetail> mapped)
        {
            var result = _companyRepo.SaveMapping(unMapped, mapped);
            return result;
        }
        public bool UpdateSubscriptionDetail(List<Dictionary<Guid, SubscriptionDetail>> subscriptions)
        {
            return _companyRepo.UpdateSubscriptionDetail(subscriptions);
        }
        public bool SaveMarkup(SubscriptionDetail markup)
        {
             return _companyRepo.SaveMarkup(markup);
        }
        public bool CheckCompanyTable()
        {
            return _companyRepo.CheckCompanyTable();
        }
        public void addflag()
        {
            _companyRepo.addflag();
        }
    }
}
