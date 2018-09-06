using PrivateLabelLite.Data.Repository.OrderRepo;
using PrivateLabelLite.Entities.Order;
using PrivateLabelLite.Entities.Product;
using PrivateLabelLite.Entities.User;
using PrivateLabelLite.Services.PartnerApi;
using PrivateLabelLite.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Services.Order
{
    public class OrderService : IOrderService
    {
        #region Fields
        private readonly IOrderRepository _orderRepository;
        private readonly IPartnerApi _partnerApi;
        private readonly IProductService _productService;
        #endregion

        #region Ctor
        public OrderService(IOrderRepository orderRepository, IPartnerApi partnerApi, IProductService productService)
        {
            this._orderRepository = orderRepository;
            this._partnerApi = partnerApi;
            this._productService = productService;

        }
        #endregion


        public bool DoesOrderBelongsToUser(string orderNo, LoggedInUserInfo userInfo)
        {
            return _orderRepository.IsOrderBelongsToUser(orderNo, userInfo);
        }


        public void RemoveUnknownMSProductsAndUpdateMissingInfo(OrderDetail orderDetail)
        {
            //var microsoftCatalogue = _partnerApi.GetMicrosoftVendorCatalogue();

            //var skuList = new List<SKU>();
            //microsoftCatalogue.VendorCatalogue.Listings.ForEach(x =>
            //{
            //    skuList.AddRange(x.skus);
            //});
            //// loop for add on; linq query not allowing modification 

            //orderDetail.Lines.RemoveAll(y => !skuList.Exists(x => x.sku == y.SKU));

            //for (int i = 0; i < skuList.Count; i++)
            //{
            //    for (int j = 0; j < orderDetail.Lines.Count; j++)
            //    {
            //        if (orderDetail.Lines[j] != null && skuList[i].sku == orderDetail.Lines[j].SKU)
            //        {
            //            orderDetail.Lines[j].SkuName = skuList[i].SkuName;
            //            // remove unknown microsoft addons
            //            if (orderDetail.Lines[j].AddOns != null)
            //            {
            //                orderDetail.Lines[j].AddOns.RemoveAll(x => !skuList[i].AddOns.Exists(y => y.sku == x.sku));
            //                for (int k = 0; k < orderDetail.Lines[j].AddOns.Count; k++)
            //                {
            //                    orderDetail.Lines[j].AddOns[k].SkuName = skuList[i].AddOns.Where(x => x.sku == orderDetail.Lines[j].AddOns[k].sku).Select(x => x.SkuName).FirstOrDefault();
            //                }
            //            }
            //        }
            //    }
            //}

        }

        public void RemoveUnknownMSProductsAndUpdateMissingInfoFromDb(OrderDetail orderDetail)
        {
            // get all products (microsoft products)
            var products = _productService.GetProducts();
            //var subscriptionProducts = _orderRepository.GetProductsFromSubscriptionSummary(orderDetail.Lines[0].SKU);

            if (orderDetail != null && orderDetail.Lines != null)
            {
                // update line info
                orderDetail.Lines.ForEach(x =>
                {
                    var sku = products.Where(p => p.Sku == x.SKU).FirstOrDefault();
                    x.SkuName = sku != null ? sku.SkuName : x.SkuName;

                    // update addOn info
                    if (!(x == null || x.AddOns == null))
                    {
                        x.AddOns.ForEach(y =>
                        {
                            var addonSku = products.Where(p => p.Sku == y.sku).FirstOrDefault();
                            y.SkuName = addonSku != null ? addonSku.SkuName : y.SkuName;
                        });
                    }
                });
            }

        }

        public OrderSearchResult GetOrders(OrderFilter filter, LoggedInUserInfo userInfo)
        {
            return _orderRepository.GetOrders(filter,userInfo);
        }


        public bool UpdateOrdersInfo(List<OrderDetail> orders)
        {
            return _orderRepository.UpdateOrdersInfo(orders);
        }

        public OrderDetail GetOrderDetail(string orderNumber)
        {
            return _orderRepository.GetOrderDetail(orderNumber);
        }
        public bool checkDatabase()
        {
            var response = _orderRepository.checkDatabase();
            return response;
        }
       public OrderDetail GetUnitPrice(OrderDetail details)
        {
            var prices = _orderRepository.GetUnitPrice(details);
            return prices;
        }
        public bool IsUserAuthorizeToIncreaseSeat(OrderLine orderLine, string ordernumber,int originalQuantity)
        {
            return _orderRepository.IsUserAuthorizeToIncreaseSeat(orderLine,ordernumber, originalQuantity);
        }
        public bool UpdateSeatCountForDay(OrderLine orderLine, string ordernumber, int originalQuantity)
        {
            return _orderRepository.UpdateSeatCountForDay(orderLine ,  ordernumber, originalQuantity);
        }
    }
}
