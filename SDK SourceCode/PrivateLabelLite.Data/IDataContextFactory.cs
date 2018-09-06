using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PrivateLabelLite.Data
{
   public interface IDataContextFactory
    {
       PrivateLabelLite.Data.DataEntities.PrivateLabelLiteDataEntities PLLDataContext();
    }
}
