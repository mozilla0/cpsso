using PrivateLabelLite.Data.DataEntities;
using PrivateLabelLite.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateLabelLite.Framework.Helper;
using System.Xml.Linq;

namespace PrivateLabelLite.Data.Repository.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        #region privateProperties
        private readonly PrivateLabelLiteDataEntities _pllContext;

        #endregion

        #region Ctor
        public ProductRepository(IDataContextFactory dataContextFactory)
        {
            this._pllContext = dataContextFactory.PLLDataContext();
        }
        #endregion

        public bool UpdateProducts(List<ProductDetail> products)
        {
            var status = true;

            var xml = new XElement("Products");
            try
            {
                foreach (var product in products)
                {
                    xml.Add(new XElement("Product",
                            new XAttribute("Sku",product.Sku.ToStringValue()),
                            new XAttribute("SkuName", product.SkuName.ToStringValue()),
                            new XAttribute("ManufacturerPartNumber", product.ManufacturerPartNumber.ToStringValue()),
                            new XAttribute("Article", product.Article.ToStringValue()),
                            new XAttribute("VendorMapId", product.VendorMapId.ToStringValue()),
                            new XAttribute("ProductType", product.ProductType.ToStringValue()),
                            new XAttribute("QtyMin", product.QtyMin.ToInt()),
                            new XAttribute("QtyMax", product.QtyMax.ToInt()),
                            new XAttribute("LastUpdated",DateTime.Now)
                        ));

                }
                _pllContext.procXmlUpsertProducts(xml.ToString());
            }
            catch (Exception)
            {

                status = false;
            }

            return status;
        }


        public DateTime GetLastUpdatedProductTimestamp()
        {
            return _pllContext.Product.Max(x => x.LastUpdated).ToDateTime();
        }


        public List<ProductDetail> GetProducts()
        {
            var productData = _pllContext.Product.ToList();
            List<ProductDetail> products = new List<ProductDetail>();
            if (productData != null)
            {
                productData.ForEach(x =>
                {
                    products.Add(new ProductDetail()
                    {
                        Id = x.Id,
                        Sku = x.Sku,
                        SkuName = x.SkuName,
                        ManufacturerPartNumber = x.ManufacturerPartNumber,
                        ProductType = x.ProductType,
                        QtyMax = x.QtyMax.ToStringValue(),
                        QtyMin = x.QtyMin.ToStringValue(),
                        LastUpdated = x.LastUpdated.ToDateTime(),
                        VendorMapId = x.VendorMapId,
                        Article = x.Article,

                    });
                });
            }
            return products;
        }
    }
}
