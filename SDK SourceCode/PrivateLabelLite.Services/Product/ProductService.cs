using PrivateLabelLite.Data.Repository.ProductRepo;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Services.Caching;
using PrivateLabelLite.Services.PartnerApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Services.Product
{
    public class ProductService : IProductService
    {
        #region prop
        private readonly IProductRepository _productRepo;
        private readonly IPartnerApi _partnerApi;
        private readonly ICacheService _cacheService;
        private const string LastUpdatedProductTimestamp_cacheKey = "10001-LastUpdatedProductTimestamp";
        #endregion

        #region Ctor
        public ProductService(IProductRepository productRepo, IPartnerApi partnerApi, ICacheService cachingService)
        {
            _productRepo = productRepo;
            this._partnerApi = partnerApi;
            _cacheService = cachingService;
        }
        #endregion


        #region Public Methods
        public bool UpdateProducts(ProductUpdateType type = ProductUpdateType.Update)
        {

            var lastUpdatedProductTimestamp = Convert.ToDateTime(GetLastUpdatedProductTimestamp(type));
            lastUpdatedProductTimestamp = lastUpdatedProductTimestamp.Year < 2000 ? DateTime.Now.AddDays(-5) : lastUpdatedProductTimestamp;
            var lastUpdatedProductHours = DateTime.Now.Subtract(lastUpdatedProductTimestamp).TotalHours;
            var intervalHours = ConfigKeys.UpdateVendorCatalogIntervalHours == 0 ? 1 : ConfigKeys.UpdateVendorCatalogIntervalHours;
            if (lastUpdatedProductHours >= intervalHours)
            {
                SetLastUpdatedProductTimestamp();
                var products = GetProductViaApiCall();
                if (products != null)
                {
                    _productRepo.UpdateProducts(products);
                }
            }
            return true;
        }

        private DateTime? GetLastUpdatedProductTimestamp(ProductUpdateType type)
        {
            // initialization of product is one time process and after it only update will happen 
            var data = new DateTime?();
            var _cacheKey = LastUpdatedProductTimestamp_cacheKey;

            if (type == ProductUpdateType.Initialize)
            {
                data = _productRepo.GetLastUpdatedProductTimestamp();
                // if there is products in db then year should be greated then 2000
                if (data.Value.Year > 2000)
                {
                    // make last updated now 
                    data = DateTime.Now;
                }
                return data;
            }
            else
            {
                data = _cacheService.Get<DateTime?>(_cacheKey);
                if (data != null && data.Value != null && data.Value.Year > 2000)
                {
                    return data;
                }
                data = _productRepo.GetLastUpdatedProductTimestamp();
            }
            _cacheService.Set(_cacheKey, data, 500);
            return data;
        }
        private void SetLastUpdatedProductTimestamp()
        {
            var _cacheKey = LastUpdatedProductTimestamp_cacheKey;
            _cacheService.Remove(_cacheKey);
            _cacheService.Set(_cacheKey, DateTime.Now, 300);
        }

        public List<Entities.Product.ProductDetail> GetProducts()
        {
            var cacheKey = "10001-Products";
            var data = _cacheService.Get<List<Entities.Product.ProductDetail>>(cacheKey);
            if (data != null)
            {
                return data;
            }
            data = _productRepo.GetProducts();

            // call update products if there is no product in database;
            if (data == null || data.Count == 0)
            {
                UpdateProducts();
            }
            _cacheService.Set(cacheKey, data);
            return data;
        }

        #endregion

        #region "Private Methods"
        private List<Entities.Product.ProductDetail> GetProductViaApiCall()
        {
            var products = new List<PrivateLabelLite.Entities.Product.ProductDetail>();
            var productCatalog = _partnerApi.GetMicrosoftVendorCatalogue();
            foreach (var listing in productCatalog.VendorCatalogue.Listings)
            {
                if (listing != null && listing.skus != null)
                {
                    foreach (var sku in listing.skus)
                    {
                        if (!products.Exists(x => (x.Sku ?? "").Equals(sku.sku, StringComparison.OrdinalIgnoreCase)))
                        {
                            products.Add(new Entities.Product.ProductDetail()
                                {
                                    Sku = sku.sku,
                                    SkuName = sku.SkuName,
                                    ManufacturerPartNumber = sku.ManufacturerPartNumber,
                                    Article = sku.Article,
                                    VendorMapId = sku.VendorMapId,
                                    ProductType = sku.ProductType,
                                    QtyMin = sku.QtyMin,
                                    QtyMax = sku.QtyMax,
                                });
                        }
                        if (!(sku == null || sku.AddOns == null))
                        {
                            foreach (var addOn in sku.AddOns)
                            {
                                if (!products.Exists(x => (x.Sku ?? "").Equals(addOn.sku, StringComparison.OrdinalIgnoreCase)))
                                {
                                    products.Add(new Entities.Product.ProductDetail()
                                    {
                                        Sku = addOn.sku,
                                        SkuName = addOn.SkuName,
                                        ManufacturerPartNumber = addOn.ManufacturerPartNumber,
                                        Article = addOn.Article,
                                        VendorMapId = addOn.VendorMapId,
                                        QtyMin = addOn.QtyMin,
                                        QtyMax = addOn.QtyMax,
                                    });
                                }
                            }
                        }
                    }
                }
            }
            return products;
        }
        #endregion


    }
}
