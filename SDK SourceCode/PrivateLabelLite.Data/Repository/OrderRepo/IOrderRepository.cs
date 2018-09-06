using PrivateLabelLite.Entities.Order;
using PrivateLabelLite.Entities.Product;
using PrivateLabelLite.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Data.Repository.OrderRepo
{
   public interface IOrderRepository
    {
       bool IsOrderBelongsToUser(string orderNo, LoggedInUserInfo userInfo);
       OrderSearchResult GetOrders(OrderFilter filter, LoggedInUserInfo userInfo);
       bool UpdateOrdersInfo(List<OrderDetail> orders);
       OrderDetail GetOrderDetail(string orderNumber);
        List<ProductDetail> GetProductsFromSubscriptionSummary(string sku);
        bool checkDatabase();
        OrderDetail GetUnitPrice(OrderDetail details);
        bool IsUserAuthorizeToIncreaseSeat(Entities.Order.OrderLine orderLine, string ordernumber,int originalQuantity);

        bool UpdateSeatCountForDay(OrderLine orderLine, string ordernumber, int originalQuantity);
    }
}
