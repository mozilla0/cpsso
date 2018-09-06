using PrivateLabelLite.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateLabelLite.Entities.Common;
namespace PrivateLabelLite.Services.Product
{
    public interface IProductService
    {
        bool UpdateProducts(ProductUpdateType type = ProductUpdateType.Update);
        List<ProductDetail> GetProducts();
    }
}
