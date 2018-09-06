using PrivateLabelLite.Entities.Order;
using PrivateLabelLite.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Services.Order
{
    public interface IOrderService
    {
        bool DoesOrderBelongsToUser(string orderNo, LoggedInUserInfo userInfo);
        void RemoveUnknownMSProductsAndUpdateMissingInfo(OrderDetail orderDetail);
        OrderSearchResult GetOrders(OrderFilter filter, LoggedInUserInfo userInfo);
        bool UpdateOrdersInfo(List<OrderDetail> orders);
        void RemoveUnknownMSProductsAndUpdateMissingInfoFromDb(OrderDetail orderDetail);
        OrderDetail GetOrderDetail(string orderNumber);
        bool checkDatabase();
        OrderDetail GetUnitPrice(OrderDetail details);
        bool IsUserAuthorizeToIncreaseSeat(Entities.Order.OrderLine orderLine, string ordernumber,int originalQuantity);
        bool UpdateSeatCountForDay(OrderLine orderLine, string ordernumber, int originalQuantity);
    }
}

