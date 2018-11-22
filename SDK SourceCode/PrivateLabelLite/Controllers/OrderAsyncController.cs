using AutoMapper;
using PrivateLabelLite.Entities.Order;
using PrivateLabelLite.Entities.User;
using PrivateLabelLite.Models;
using PrivateLabelLite.Services.Order;
using PrivateLabelLite.Services.PartnerApi;
using PrivateLabelLite.Services.Product;
using PrivateLabelLite.Services.User;
using PrivateLabelLite.Services.Company;
using PrivateLabelLite.Entities.Subsciptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PrivateLabelLite.Entities.Common;

namespace PrivateLabelLite.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class OrderAsyncController : BaseController
    {
        #region Fields
        private readonly IPartnerApi _partnerApi;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly ICompanyService _companyService;
        #endregion

        #region Ctor
        public OrderAsyncController(IPartnerApi partnerApi, IOrderService orderService, IUserService userService, IProductService productService, ICompanyService companyService)
        {
            this._partnerApi = partnerApi;
            this._orderService = orderService;
            this._userService = userService;
            this._productService = productService;
            this._companyService = companyService;
        }
        #endregion

        public ActionResult GetOrders(OrderFilter filter)
        {
            filter.OrderNumbers = null;
            filter.Page = filter.Page == 0 ? 1 : filter.Page;
            var model = new CustomerOrdersModel()
            {
                Filter = filter,
            };
            var loggedInUserInfo = GetLoggedInUserInfo();
            var orderSearchResult = _orderService.GetOrders(filter, loggedInUserInfo);
            var orders = new List<OrderInfoModel>();
            if (orderSearchResult != null)
            {
                filter.TotalRecords = orderSearchResult.TotalRecords;
                orders = Mapper.Map<List<OrderInfoModel>>(orderSearchResult.OrderSearch);
            }
            if (orders != null)
            {
                model = new CustomerOrdersModel()
                {
                    Orders = orders,
                    Filter = filter
                };
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RefereshOrderDetail(string id, string companyName = "")
        {
            LoggedInUserInfo loggedInUser = GetLoggedInUserInfo();

            if (!_userService.IsEndUserMappingExist(loggedInUser) || !_orderService.DoesOrderBelongsToUser(id, loggedInUser) || string.IsNullOrEmpty(id))
            {
                return Json(new OrderDetailModel(), JsonRequestBehavior.AllowGet);
            }
            var order = _partnerApi.GetOrderDetail(id).OrderInfo;
            //var order = _orderService.GetOrderDetail(id);
            OrderDetailModel model = new OrderDetailModel();
            if (order != null)
            {
                model.OrderDetail = order;
                //remove unknownMicrosoft products and update skuName
                _orderService.RemoveUnknownMSProductsAndUpdateMissingInfoFromDb(model.OrderDetail);

                //add order to orderList
                var orderList = new List<OrderDetail>();
                orderList.Add(model.OrderDetail);
                // update order in db
                _orderService.UpdateOrdersInfo(orderList);
                model.OrderDetail = _orderService.GetOrderDetail(id);
                model.ModifyOrder = new ModifyOrder();
                model.ModifyAddOnsDetail = new ModifyOrderAddons();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateProducts()
        {
            if (Session["UpdateProducts"] == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            Session["UpdateProducts"] = null;
            _productService.UpdateProducts();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}