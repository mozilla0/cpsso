using PrivateLabelLite.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Data.Repository.ProductRepo
{
   public interface IProductRepository
    {
       bool UpdateProducts(List<ProductDetail> products);
       DateTime GetLastUpdatedProductTimestamp();
       List<ProductDetail> GetProducts();
    }
}
