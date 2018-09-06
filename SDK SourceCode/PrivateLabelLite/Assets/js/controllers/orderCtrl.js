(function () {

    'use strict';
    app.controller('orderCtrl', orderCtrl);
    orderCtrl.$inject = ["$scope", "orderData", "$rootScope", "$timeout"];
    function orderCtrl($scope, orderData, $rootScope, $timeout) {
        $scope.isAuthorized = false;
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
        $scope.confirmOrderQuantityUpdate = function (orderNumber, isReseller, orderLine, addOn) {
            if (orderLine.lineStatus != "active") {
                $("#NotActiveSubscription").modal();
                $scope.NotActiveSubscriptionMessage = "Your Subscription is not Active, Please contact your Administrator."
            }
            else {
                if (isReseller || addOn) {
                    $scope.isSuccess = false;
                    $scope.updateOrderLine = orderLine;
                    $scope.updateAddOn = addOn;
                    var quantity = 0, sku = '', type = '', originalQuantity = '';
                    if (addOn) {
                        type = "addOn";
                        quantity = addOn.quantity;
                        sku = addOn.sku;
                        originalQuantity = addOn.originalQuantity;
                    }
                    else {
                        quantity = orderLine.quantity;
                        sku = orderLine.sku;
                        originalQuantity = orderLine.originalQuantity;
                    }

                    $scope.orderLineToUpdate = orderLine;
                    if (addOn) {
                        $("#confirmationAddOnPopup").modal();
                    }
                    else {
                        $scope.popupTitle = 'Update Order Quantity';
                        $("#confirmationPopup").modal();
                    }

                    $scope.message = "Are you sure you want to change order" + type + " quantity of SKU " + sku + " from " + originalQuantity + " to " + quantity + " ?";
                }
                else //if not reseller than calculate timestamp and check for seat limit
                {
                    $scope.isSuccess = false;
                    $scope.updateOrderLine = orderLine;
                    //$scope.updateAddOn = addOn;
                    var quantity = 0, sku = '', type = '', originalQuantity = '';
                    quantity = orderLine.quantity;
                    sku = orderLine.sku;
                    originalQuantity = orderLine.originalQuantity;
                    $scope.orderLineToUpdate = orderLine;
                    $scope.orderNumber = orderNumber;
                    $scope.popupTitle = 'Update Quantity';
                    $("#confirmationPopupForEndUser").modal();
                    $scope.confirmationPopupMessageForEndUser = "Are you sure you want to change order" + type + " quantity of SKU " + sku + " from " + originalQuantity + " to " + quantity + " ?";
                    //$scope.isUserAuthorizeToIncreaseSeat(orderLine, orderNumber);
                }
            }
        };

        $scope.setVariables = function () {
            $scope.logHistory = true;
            $scope.Orderline = false;
            $scope.showDownload = true;
            $scope.more = false;
        }

        $scope.saveOrderQuantity = function (order, isReseller) {
            if (isReseller) {
                var token = angular.element('input[name="__RequestVerificationToken"]').attr('value');
                var modifyorder = new FormData();
                modifyorder.append("endUserEmail", order.endUserEmail),
                modifyorder.append("endUserName", order.endUserName),
                modifyorder.append("action", "units"),
                modifyorder.append("orderNumber", order.orderNumber),
                modifyorder.append("sku", $scope.orderLineToUpdate.sku),
                modifyorder.append("skuName", $scope.orderLineToUpdate.skuName),
                modifyorder.append("originalQuantity", $scope.orderLineToUpdate.originalQuantity),
                modifyorder.append("newQuantity", $scope.orderLineToUpdate.quantity),
                modifyorder.append("metaData.firstName", $scope.user.firstName),
                modifyorder.append("metaData.lastName", $scope.user.lastName),
                modifyorder.append("metaData.isEndCustomer", $scope.user.isEndCustomer),
               modifyorder.append('__RequestVerificationToken', token);

                //make order modification call
                orderData.modifyOrderDetail(modifyorder).then(function (resp) {
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
            }
            else {
             
                orderData.isUserAuthorizeToIncreaseSeat($scope.orderLineToUpdate, $scope.orderNumber, $scope.orderLineToUpdate.originalQuantity).then(function (resp) {
                    if (resp.data == "True" || $scope.orderLineToUpdate.quantity < $scope.orderLineToUpdate.originalQuantity) {
                        var token = angular.element('input[name="__RequestVerificationToken"]').attr('value');
                        var modifyorder = new FormData();
                        modifyorder.append("endUserEmail", order.endUserEmail),
                        modifyorder.append("endUserName", order.endUserName),
                        modifyorder.append("action", "units"),
                        modifyorder.append("orderNumber", order.orderNumber),
                        modifyorder.append("sku", $scope.orderLineToUpdate.sku),
                        modifyorder.append("skuName", $scope.orderLineToUpdate.skuName),
                        modifyorder.append("originalQuantity", $scope.orderLineToUpdate.originalQuantity),
                        modifyorder.append("newQuantity", $scope.orderLineToUpdate.quantity),
                        modifyorder.append("metaData.firstName", $scope.user.firstName),
                        modifyorder.append("metaData.lastName", $scope.user.lastName),
                        modifyorder.append("metaData.isEndCustomer", $scope.user.isEndCustomer),
                       modifyorder.append('__RequestVerificationToken', token);

                        //make order modification call
                        orderData.modifyOrderDetail(modifyorder).then(function (resp) {
                            if (resp.data.isValid) {
                                $scope.popupTitle = 'Quantity Updated';
                                $scope.confirmationPopupMessageForEndUser = "Your request has been placed successfully. Please wait a few seconds for quantity update.";
                                $scope.orderLineToUpdate.origianlQuantity = $scope.orderLineToUpdate.quantity;
                                if ($scope.orderLineToUpdate.quantity > $scope.orderLineToUpdate.originalQuantity) {
                                    orderData.updateSeatCountForDay($scope.orderLineToUpdate, $scope.orderNumber, $scope.orderLineToUpdate.originalQuantity).then(function (resp) {
                                    })
                                }
                                $timeout(function () {
                                    $scope.refreshOrderDetail();
                                }, 8000);
                             
                            }
                            else {
                                $scope.message = "Something went wrong. Try Again.";
                            }
                            $scope.isSuccess = true;
                        })
                        .catch(function () {

                        });
                    }
                    else {
                       // $("#seatExhaustedLimit").modal();
                        //$scope.seatExhaustedLimitMessage = "You have reached the maximum amount of seats that can be increased per day. Wait for 24 hours to purchase more or contact us manual purchase. ";
                        $scope.popupTitle = 'Seat Limit : ';
                        //$("#confirmationPopupForEndUser").modal();
                        $scope.confirmationPopupMessageForEndUser = "You have reached the maximum amount of seats that can be increased per day. Wait for 24 hours to purchase more or contact us manual purchase. ";
                        $scope.isSuccess = true;
                        $timeout(function () {
                            $scope.refreshOrderDetail();
                        }, 8000);
                    }

                }).catch(function (resp) {

                })
            
            }

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
            var modifyAddons = {};
            //$scope.model.modifyAddOnsDetail.modifyAddons = {};
            //$scope.model.modifyAddOnsDetail.modifyAddons.orderNumber = order.orderNumber;
            //$scope.model.modifyAddOnsDetail.modifyAddons.endUserEmail = order.endUserEmail;
            //$scope.model.modifyAddOnsDetail.modifyAddons.endUserName = order.endUserName;
            //$scope.model.modifyAddOnsDetail.modifyAddons.baseSubscription = orderLine.sku;
            //$scope.model.modifyAddOnsDetail.modifyAddons.addons = [];
            //$scope.model.modifyAddOnsDetail.modifyAddons.addons.push({
            //    action: "units",
            //    addOnSku: addOn.sku,
            //    newQuantity: addOn.quantity,
            //    originalQuantity: addOn.originalQuantity,
            //    skuName: addOn.skuName
            //});
            //$scope.model.modifyAddOnsDetail.modifyAddons.metaData = {
            //    firstName: $scope.user.firstName,
            //    lastName: $scope.user.lastName,
            //    isEndCustomer: $scope.user.isEndCustomer
            //};

            var token = angular.element('input[name="__RequestVerificationToken"]').attr('value');
            var modifyAddOnsDetail = new FormData();
            modifyAddOnsDetail.append("modifyAddons.orderNumber", order.orderNumber),
            modifyAddOnsDetail.append("modifyAddons.endUserEmail", order.endUserEmail),
            modifyAddOnsDetail.append("modifyAddons.endUserName", order.endUserName),
            modifyAddOnsDetail.append("modifyAddons.orderNumber", order.orderNumber),
            modifyAddOnsDetail.append("modifyAddons.baseSubscription", orderLine.sku),
            modifyAddOnsDetail.append("modifyAddons.addon.action", "units"),
            modifyAddOnsDetail.append("modifyAddons.addon.addOnSku", addOn.sku),
            modifyAddOnsDetail.append("modifyAddons.addon.newQuantity", addOn.quantity),
            modifyAddOnsDetail.append("modifyAddons.addon.originalQuantity", addOn.originalQuantity),
            modifyAddOnsDetail.append("modifyAddons.addon.skuName", addOn.skuName),
            modifyAddOnsDetail.append("modifyAddons.metaData.firstName", $scope.user.firstName),
            modifyAddOnsDetail.append("modifyAddons.metaData.lastName", $scope.user.lastName),
            modifyAddOnsDetail.append("modifyAddons.metaData.isEndCustomer", $scope.user.isEndCustomer),
            modifyAddOnsDetail.append('__RequestVerificationToken', token);


            //make order addOn modification call
            orderData.modifyOrderAddOns(modifyAddOnsDetail).then(function (resp) {
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
            $scope.isAnnualSubscription(orderNo.orderDetail);
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
                angular.forEach($scope.model.orderDetail.lines, function (line) {
                    var index;
                    //var index = $scope.lines.findIndex(i => i.sku == line.sku);
                    angular.forEach($scope.lines, function (item, i) {
                        if(item.sku == line.sku)
                        {
                            index = i;
                        }
                    });
                    if (index >= 0) {
                        line.unitPrice = $scope.lines[index].unitPrice;
                        line.total = $scope.lines[index].unitPrice * line.quantity;
                        line.salesPrice = $scope.lines[index].salesPrice;
                        line.totalSalesPriceEndUser = $scope.lines[index].salesPrice * line.quantity;
                        line.taxStatus = $scope.lines[index].taxStatus;
                        line.seatLimit = $scope.lines[index].seatLimit;
                        line.seatLimitStartTime = $scope.lines[index].seatLimitStartTime;
                        line.seatLimitEndTime = $scope.lines[index].seatLimitEndTime;
                        line.seatCounter = $scope.lines[index].seatCounter;

                    }
                });
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
        };
        $scope.hideSearchBar = function () {
            $('#search-xs').removeClass('in');
        };

        $scope.companies = [];
        $scope.companyList = function (model) {
            for (var i = 0; i < model.orders.length; i++) {
                $scope.companies.push(model.orders[i].company.companyName);
            }
        };
        $scope.getUserSubscriptions = function () {
            $scope.userAuthorized = true;
            orderData.getUserSubscriptions($scope.model.filter).then(function (resp) {
                if (resp.data.isValid == false && resp.data.message == "End user mapping does not exist") {
                    $scope.userNotMapped = true;
                    $scope.userAuthorized = false;
                }
                else {
                    if (resp.data.subscriptionList.length > 0) {
                        $scope.model.subscriptionList = resp.data.subscriptionList;
                        $scope.userAuthorized = true;
                    }

                    else {
                        $scope.userAuthorized = false;
                        $scope.userNotMapped = false;
                    }
                }
                //$scope.showTableHeading = true;
            })
           .catch(function () {
           });
        };

        $scope.setData = function (orderNumber) {
            $scope.logHistory = true;
            $scope.Orderline = false;
            window.location.href = "/Order/DownloadSubscriptionHistory/" + orderNumber;
        };

        $scope.showOrderDetail = function () {
            $scope.logHistory = false;
            $scope.showDownload = false;
            $scope.showMore = true;
            angular.forEach($scope.model.orderDetail.lines, function (line) {
                line.itemLimit = 3;
                line.showMore = true;
                angular.forEach(line.addOns, function (addOn) {
                    addOn.itemLimitAddOn = 3;
                    addOn.showMoreAddOn = true;
                })
            });
            $scope.showMore = true;

        };

        $scope.viewLogHistory = function () {
            $scope.logHistory = true;
            $scope.Orderline = false;
            $scope.showDownload = true;
            $scope.showMore = true;
        };

        $scope.isAnnualSubscription = function (orderDetail) {

            if (orderDetail.lines.length > 0) {
                var manufacturerNo = orderDetail.lines[0].manufacturerPartNumber;
                if (manufacturerNo[manufacturerNo.length - 1].toLowerCase() == "x") {
                    $scope.isAnnual = true;
                }
                else {
                    $scope.isAnnual = false;
                }

            }
        };
        $scope.initUnitPrice = function (orderDetail) {
            if (orderDetail != null) {
                orderData.initUnitPrice(orderDetail).then(function (resp) {
                    $scope.lines = resp.data;

                    angular.forEach($scope.model.orderDetail.lines, function (line) {
                        var index;
                        //var index = $scope.lines.findIndex(i => i.sku == line.sku);
                        angular.forEach($scope.lines, function (item, i) {
                            if (item.sku == line.sku) {
                                index = i;
                            }
                        });
                        if (index >= 0) {
                            line.unitPrice = $scope.lines[index].unitPrice;
                            line.total = $scope.lines[index].unitPrice * line.quantity;
                            line.salesPrice = $scope.lines[index].salesPrice;
                            line.totalSalesPriceEndUser = $scope.lines[index].salesPrice * line.quantity;
                            line.taxStatus = $scope.lines[index].taxStatus;
                            line.seatLimit = $scope.lines[index].seatLimit;
                            line.seatLimitStartTime = $scope.lines[index].seatLimitStartTime;
                            line.seatLimitEndTime = $scope.lines[index].seatLimitEndTime;
                            line.seatCounter = $scope.lines[index].seatCounter;


                        }
                    });

                }).catch(function () {

                })
            }
        };
    };
})();

