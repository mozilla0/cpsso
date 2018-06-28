(function () {

    'use strict';
    app.controller('orderCtrl', orderCtrl);
    orderCtrl.$inject = ["$scope", "orderData","$rootScope","$timeout"];
    function orderCtrl($scope, orderData, $rootScope,$timeout) {
        $scope.showEndUserPopup = function () {
            $("#endUserPopup").modal();
            $scope.isSuccess = false;
        };
        $scope.updateOrderQuantity = function (orderLine, updateType) {
            if (updateType > 0) {
                orderLine.quantity = parseInt(orderLine.quantity) + 1;
            }
            else if (orderLine.quantity > 0)
                orderLine.quantity = parseInt(orderLine.quantity) - 1;
        }
        $scope.confirmOrderQuantityUpdate = function (orderLine, addOn) {
            $scope.isSuccess = false;
            $scope.updateOrderLine = orderLine;
            $scope.updateAddOn = addOn;
            var quantity = 0, sku = '', type = '';
            if (addOn) {
                type = "addOn";
                quantity = addOn.quantity;
                sku = addOn.sku;
            }
            else {
                quantity = orderLine.quantity;
                sku = orderLine.sku;
            }

            $scope.orderLineToUpdate = orderLine;
            if (addOn) {
                $("#confirmationAddOnPopup").modal();
            }
            else {
                $scope.popupTitle = 'Update Order Quantity';
                $("#confirmationPopup").modal();
            }

            $scope.message = "Are you sure you want to change order" + type + " quantity of SKU " + sku + " to " + quantity + " ?";
        };
        $scope.saveOrderQuantity = function (order) {
            $scope.model.modifyOrder.modifyOrders = [];
            $scope.model.modifyOrder.modifyOrders.push({ endUserEmail: order.endUserEmail, endUserName: order.endUserName, action: "units", orderNumber: order.orderNumber, sku: $scope.orderLineToUpdate.sku, skuName: $scope.orderLineToUpdate.skuName, originalQuantity: $scope.orderLineToUpdate.originalQuantity, newQuantity: $scope.orderLineToUpdate.quantity, metaData: { firstName: $scope.user.firstName, lastName: $scope.user.lastName, isEndCustomer: $scope.user.isEndCustomer } });

            //make order modification call
            orderData.modifyOrderDetail($scope.model.modifyOrder).then(function (resp) {
                if (resp.data.isValid) {
                    $scope.popupTitle = 'Quantity Updated';
                    $scope.message = "Your request has been placed successfully. Please wait a few seconds for quantity update.";
                    $scope.orderLineToUpdate.origianlQuantity = $scope.orderLineToUpdate.quantity;
                    $timeout(function () {
                        $scope.refreshOrderDetail();
                    }, 13000);
                }
                else {
                    $scope.message = "Something went wrong. Try Again.";
                }
                $scope.isSuccess = true;
            })
            .catch(function () {

            });
        };

        $scope.updateAddOnQuantity = function (addOn, updateType) {
            if (updateType > 0) {
                addOn.quantity = parseInt(addOn.quantity) + 1;
            }
            else if (addOn.quantity > 0)
                addOn.quantity = parseInt(addOn.quantity) - 1;
        };

        $scope.saveAddOnQuantity = function (order) {

            var orderLine = $scope.updateOrderLine;
            var addOn = $scope.updateAddOn;

            $scope.model.modifyAddOnsDetail.modifyAddons = {};
            $scope.model.modifyAddOnsDetail.modifyAddons.orderNumber = order.orderNumber;
            $scope.model.modifyAddOnsDetail.modifyAddons.endUserEmail = order.endUserEmail;
            $scope.model.modifyAddOnsDetail.modifyAddons.endUserName = order.endUserName;
            $scope.model.modifyAddOnsDetail.modifyAddons.baseSubscription = orderLine.sku;
            $scope.model.modifyAddOnsDetail.modifyAddons.addons = [];
            $scope.model.modifyAddOnsDetail.modifyAddons.addons.push({
                action: "units",
                addOnSku: addOn.sku,
                newQuantity: addOn.quantity,
                originalQuantity: addOn.originalQuantity,
                skuName: addOn.skuName
            });
            $scope.model.modifyAddOnsDetail.modifyAddons.metaData = {
                firstName: $scope.user.firstName,
                lastName: $scope.user.lastName,
                isEndCustomer: $scope.user.isEndCustomer
            };

            //make order addOn modification call
            orderData.modifyOrderAddOns($scope.model.modifyAddOnsDetail).then(function (resp) {
                if (resp.data.isValid) {
                    $scope.message = "Your request has been placed successfully. Please wait a few seconds for quantity update.";
                    $scope.updateAddOn.origianlQuantity = $scope.updateAddOn.quantity;
                    $timeout(function () {
                        $scope.refreshOrderDetail();
                    }, 13000);
                }
                else {
                    $scope.message = "Something went wrong. Try Again.";
                }
                $scope.isSuccess = true;
            })
            .catch(function () {

            });
        };
        $scope.getOrders = function () {
            $rootScope.hideLoader = true;
            orderData.getOrders($scope.model.filter).then(function (resp) {
                $scope.model.orders = resp.data.orders;
                $scope.model.filter = resp.data.filter;
                $scope.isSuccess = true;
                $scope.initializingSalesOrders = false;
            })
            .catch(function () {
            });
        };
        $scope.initSalesOrder = function () {
            $scope.initializingSalesOrders = true;
            //$scope.getOrders();
            $scope.updateProductCall();
        };
        $scope.initOrderDetail = function (orderNo, userName, userEmail, isReseller) {
           
            //orderData.getOrderDetail(orderNo).then(function (resp) {
            //    $scope.model = resp.data;
            //    $scope.issuccess = true;
            //    $scope.refreshingOrderDetail = false;
            //})
            //.catch(function () {

            //});
            $scope.refreshOrderDetail();
            $scope.updateProductCall();
            $scope.user = { firstName: userName, lastName: userEmail, isEndCustomer: isReseller };
        };
        $scope.refreshOrderDetail = function () {
            $scope.refreshingOrderDetail = true;
            orderData.refereshOrderDetail($scope.model.orderDetail.orderNumber).then(function (resp) {
                $scope.model.orderDetail = resp.data.orderDetail;
                $scope.isSuccess = true;
                $scope.refreshingOrderDetail = false;
            })
            .catch(function () {

            });
        };

        $scope.updateProductCall = function () {
            $rootScope.hideLoader = true;
            orderData.updateProductInfo().then(function (resp) {
            })
            .catch(function () {
            });
        }
        $scope.hideSearchBar = function() {
            $('#search-xs').removeClass('in');
        }

    };

})();