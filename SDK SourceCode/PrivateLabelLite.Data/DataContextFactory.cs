using PrivateLabelLite.Data.DataEntities;
using System;
using System.Configuration;
namespace PrivateLabelLite.Data
{
    public class DataContextFactory : IDataContextFactory
    {
        public PrivateLabelLiteDataEntities PLLDataContext()
        {
            return new PrivateLabelLiteDataEntities();
        }
    }
}
