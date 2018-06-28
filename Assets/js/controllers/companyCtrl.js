(function () {

    'use strict';
    app.controller('companyCtrl', companyCtrl);
    app.constant('CONSTANTS', {  ALL_COMPANY: 'ALL' });
    companyCtrl.$inject = ["$scope", "companyData", "$rootScope","CONSTANTS"];
    function companyCtrl($scope, companyData, $rootScope, CONSTANTS) {
        $scope.checkedUsers = [];
        $scope.toBeMapped = [];
        $scope.toBeUnMapped = [];
        $scope.showTableHeading = false;
        $scope.company = null;
        $scope.showEndUserPopup = function () {
            $scope.model.user = { email: '' };
            $("#endUserPopup").modal();
            $scope.isSuccess = false;
            $scope.message = '';
            $scope.addEditUserHeader = 'Add App User';
        };


        $scope.addEndUserMapping = function (model) {
            var user = model.user;
            var companyName = user.companyName || $scope.model.user.selectedCompany.companyName;
            var companyId = user.selectedCompany ? user.selectedCompany.companyId : null;
            var createdBy = user.createdBy ? user.createdBy : null;
            var created = user.created ? user.created : null;
            var id = user.id ? user.id : null;

            user.token = angular.element('input[name="__RequestVerificationToken"]').attr('value');
            var companyUser = new FormData();
            companyUser.append("companyName", companyName);
            companyUser.append("email", user.email);
            companyUser.append("name", user.name);
            companyUser.append("companyId", companyId);
            companyUser.append("id", id);
            companyUser.append("createdBy", createdBy);
            companyUser.append("created", created);
            companyUser.append("companyId", companyId);
            //companyUser.append("companyName", user.selectedCompany.companyName);
            companyUser.append('__RequestVerificationToken', user.token);

            companyData.addEndUserMapping(companyUser).then(function (resp) {
                if (resp.data.isValid) {
                    $scope.message = "User Saved Successfully";
                    $scope.isSuccess = true;
                    $scope.getCompanyEndUsers($scope.model.filter);
                } else $scope.message = resp.data.message;
            })
            .catch(function () {

            });
        };
        $scope.editSalesOrderMapping = function (companyOrder) {
            if (companyOrder != null) {
                $scope.editCompanyOrder = true;
                companyOrder.companyId = companyOrder.companyId.toString();
                $scope.model.companyOrder.companyName = companyOrder.companyName;
                $scope.model.selectedCompany = { companyId: companyOrder.companyId, companyName: companyOrder.companyName };
                $scope.headerText = "Edit Sales Order";
            }
            else {
                $scope.editCompanyOrder = false;
                $scope.headerText = "Add Sales Order";
            }
            $scope.model.companyOrder = angular.extend({}, companyOrder);
            $("#salesOrderMappingPopup").modal();
            $scope.isSuccess = false;
            $scope.message = null;
            $scope.errorMessage = null;
        };
        $scope.upsertCompanyOrderMapping = function (model) {
            //var companyOrder = { companyId: model.companyOrder.companyId, salesOrderIdCSV: model.companyOrder.salesOrderIdCSV, salesOrderId: model.companyOrder.salesOrderId, recordId: model.companyOrder.recordId };
            var companyOrder = {};
            angular.extend(companyOrder, model.companyOrder);
            companyOrder.companyId = model.selectedCompany.companyId;
            companyOrder.token = angular.element('input[name="__RequestVerificationToken"]').attr('value');
            var companyForm = new FormData();

            companyForm.append("companyId", companyOrder.companyId);
            companyForm.append('companyName', companyOrder.companyName);
            companyForm.append('salesOrderIdCSV', companyOrder.salesOrderIdCSV);
            companyForm.append('__RequestVerificationToken', companyOrder.token);
            companyData.upsertCompanyOrderMapping(companyForm).then(function (resp) {
                if (resp.data.isValid) {
                    $scope.message = resp.data.message;
                    $scope.errorMessage = (resp.data.errorMessage || "").split('#');
                    $scope.isSuccess = true;
                    $scope.getCompanyOrders($scope.model.filter);
                }
                else {
                    $scope.message = resp.data.message;
                    $scope.isSuccess = false;
                }

            })
            .catch(function () {

            });
        };
        $scope.getCompanyEndUsers = function (item) {
            if (item) {

                $scope.model.filter.companyId = item.companyId;
                $scope.model.filter.companyName = item.companyName;
            } else {
                if (item == undefined) {
                    $scope.model.filter.selectedCompany = null;
                }

                $scope.model.filter.companyId = $scope.model.filter.selectedCompany ? $scope.model.filter.selectedCompany.companyId : 0;
                $scope.model.filter.companyName = $scope.model.filter.selectedCompany ? $scope.model.filter.selectedCompany.companyName : null;
            }
            companyData.getCompanyEndUsers($scope.model.filter).then(function (resp) {
                if (resp && resp.data) {
                    $scope.model.endUsers = resp.data;
                    $scope.model.filter.totalRecords = resp.data.length > 0 ? resp.data[0].totalRecords : 0;
                }
            })
            .catch(function () {

            });
        };
        $scope.getCompanyOrders = function (filter) {
            companyData.getCompanyOrders(filter).then(function (resp) {
                $scope.model.companyOrders = resp.data;
                for (var i = 0; i < $scope.model.companyOrders.length; i++) {
                    if ($scope.checkedUsers != null) {
                        for (var j = 0; j < $scope.checkedUsers.length; j++) {
                            if ($scope.checkedUsers[j] == $scope.model.companyOrders[i].recordId)
                                $scope.model.companyOrders[i].checked = true;
                        }
                    }

                }
            })
            .catch(function () {

            });
        };
        $scope.getSalesOrderIdListFromCSV = function (salesOrderIdCSV) {
            var soIds = [];
            soIds = salesOrderIdCSV > '' ? salesOrderIdCSV.split(',') : [];
            return soIds;
        };
        $scope.filterCompanyOrders = function (item) {

            if (item)
                $scope.model.filter.companyId = item.companyId;
            else {


                if (item == undefined) {
                    $scope.model.selectedCompany = null;
                }
                $scope.model.filter.companyId = $scope.model.selectedCompany ? $scope.model.selectedCompany.companyId : 0;
            }
            $rootScope.hideLoader = true;

            $scope.getCompanyOrders($scope.model.filter);
        };
        $scope.editEndUser = function (user) {
            $scope.model.user = {};
            $scope.message = '';
            $scope.addEditUserHeader = 'Edit User : ' + user.name;
            angular.extend($scope.model.user, user);
            $("#endUserPopup").modal();
            $scope.isSuccess = false;
        };

        //POP-UP TO DELETE ENDUSER
        $scope.showDeleteEndUserPopup = function (user) {
            $scope.checkedUsers.push(user.id);
            $scope.model.user = user;
            $scope.isSuccess = false;
            $scope.message = "Are you sure you want to delete the user " + user.name + " ?";
            $("#deleteEndUserPopup").modal();
        };
        $scope.showMultipleDeleteEndUserPopup = function () {

            angular.forEach($scope.model.endUsers, function (user, index) {
                if (user.checked == true) {
                    $scope.checkedUsers.push(user.id);
                }
            });

            $scope.isSuccess = false;
            $scope.message = "Are you sure you want to delete the users ?";
            $("#deleteMultipleEndUserPopup").modal();
        };
        // END OF POP-UP TO DELETE ENDUSER


        //POP-UP TO DELETE COMPANY ORDER MAPPING
        $scope.showDeleteCompanyOrderMappingPopup = function (companyOrder) {
            $scope.checkedUsers.push(companyOrder.recordId);
            //$scope.model.companyOrder = companyOrder;
            $scope.message = "Are you sure you want to delete sales order mapping for order " + companyOrder.salesOrderId;
            $scope.isSuccess = false;
            $('#deleteCompanyOrderPopup').modal();

        };
        $scope.showMultipleDeleteCompanyOrderMappingPopup = function () {

            //angular.forEach($scope.model.companyOrders, function (order, index) {
            //    if (order.checked == true) {
            //        $scope.checkedUsers.push(order.recordId);
            //    }
            //})

            $scope.message = "Are you sure you want to delete sales order mapping for selected orders ";
            $scope.isSuccess = false;
            $('#deleteMultipleCompanyOrderPopup').modal();
        };
        //END OF POP-UP TO DELETE COMPANY ORDER MAPPING

        //DELETING end-user mapping
        $scope.deleteEndUserMapping = function () {
            var token = angular.element('input[name="__RequestVerificationToken"]').attr('value');
            var Ids = '';
            angular.forEach($scope.checkedUsers, function (user, index) {
                Ids = Ids + user + ',';
            });
            var deleteUser = new FormData();
            deleteUser.append("CustomerId", Ids);
            deleteUser.append("__RequestVerificationToken", token);
            companyData.deleteEndUserMapping(deleteUser).then(function (resp) {
                if (resp.data.isValid) {
                    $scope.message = "Customer User successfully deleted";  //resp.data.message;
                    $scope.isSuccess = true;
                    $scope.getCompanyEndUsers($scope.model.filter);
                    $scope.isVisible = true;
                    $scope.showSingleDelete = false;

                }
            })
            .catch(function () {

            });
        };

        //DELETING company(sales) order mapping
        $scope.deleteCompanyOrderMapping = function () {
            var token = angular.element('input[name="__RequestVerificationToken"]').attr('value');
            var Ids = '';
            angular.forEach($scope.toBeUnMapped, function (user, index) {
                Ids = Ids + user + ',';
            });
            var deleteMapping = new FormData();
            deleteMapping.append("recordIds", Ids);
            deleteMapping.append("__RequestVerificationToken", token);
            companyData.deleteCompanyOrderMapping(deleteMapping).then(function (resp) {
                if (resp.data.isValid) {
                    $scope.message = resp.data.message;
                    $scope.isSuccess = true;
                    $scope.getCompanyOrders($scope.model.filter);
                    $scope.isVisible = true;
                    $scope.showSingleDelete = false;
                }
            })
            .catch(function () {

            });
        };

        $scope.getPaginatedUsers = function () {
            $scope.getCompanyEndUsers($scope.model.filter);
        };


        $scope.clearOrderSearch = function () {
            $scope.model.filter.salesOrderIds = null;
            $scope.model.selectedCompany = null;
            $scope.model.companyName = null;
            $scope.model.filter.selectedCompany = null;
            $scope.filterCompanyOrders();
        };

        $scope.clearCompanySearch = function () {
            $scope.model.filter.selectedCompany = null;
            $scope.getCompanyEndUsers();
        };

        $scope.hideSearchBar = function () {
            $('#search-xs').removeClass('in');
        };

        $scope.hideEditForEndUser = function (checked) {
            var itemSelected = false;
            angular.forEach($scope.model.endUsers, function (user, index) {
                if (user.checked == true) {
                    itemSelected = true;
                }
            });
            if (itemSelected) {
                $scope.isVisible = false;
                $scope.showSingleDelete = true;
            }
            else {
                $scope.isVisible = true;
                $scope.showSingleDelete = false;
            }
        };

        $scope.hideEditForSalesOrder = function () {
            $scope.selectedUser;
            var itemSelected = false;
            angular.forEach($scope.model.companyOrders, function (user, index) {
                if (user.checked == true) {
                    if (!$scope.checkedUsers.includes(user.recordId)) {
                        $scope.checkedUsers.push(user.recordId);

                    }
                    itemSelected = true;
                }
                else {
                    if ($scope.checkedUsers.includes(user.recordId)) {
                        index = $scope.checkedUsers.indexOf(user.recordId);
                        if (index >= 0) {
                            $scope.checkedUsers.splice(index, 1);
                        }
                    }

                }
            });
            if (itemSelected == true || $scope.checkedUsers.length != 0) {
                $scope.isVisible = false;
                $scope.showSingleDelete = true;
            }
            else {
                $scope.isVisible = true;
                $scope.showSingleDelete = false;
            }
        };

        $scope.refreshCompanies = function () {
            companyData.refreshCompanies().then(function (resp) {
                if (resp.data.isValid) {
                    $scope.model.companies = resp.data.data;
                }
            })
            .catch(function () {

            });
        };

        $scope.getPaginatedSubscriptions = function (model) {
                companyData.getPaginatedSubscriptions($scope.model.filter).then(function (resp) {
                    //$scope.company = $scope.model.filter.companyName;
                    $scope.model.subscriptionList = resp.data.subscriptionList;
                    $scope.showTableHeading = true;
                    $scope.model.subscriptionList.length = resp.data.subscriptionList.length;
                    if ($scope.model.subscriptionList.length > 0) {
                        $scope.showMobileView = true;
                    }

                })

                    .catch(function () {
                    });
         
        };

       

        $scope.SaveMapping = function () {

            var subscriptions = $scope.model.subscriptionList;
            angular.forEach($scope.model.subscriptionList, function (subscription, index) {
                if (subscription.mapping == true) {
                    if (!$scope.toBeMapped.indexOf(subscription.orderNumber) !== -1) {
                        $scope.toBeMapped.push(subscription.orderNumber);
                        $('#successModal').modal('show');
                    }

                }

                if (subscription.changedStatus && subscription.mappingStatus == 'MAPPED') {
                    if (!$scope.toBeUnMapped.indexOf(subscription.orderNumber) !== -1) {
                        $scope.toBeUnMapped.push(subscription.orderNumber);
                        $('#successModal').modal('show');
                        subscription.changedStatus = false;

                    }

                }
            });
            companyData.saveMapping($scope.toBeUnMapped, $scope.toBeMapped).then(function (resp) {
                $scope.toBeMapped = [];
                $scope.toBeUnMapped = [];
                $scope.getPaginatedSubscriptions();
            }).catch(function () {

            });
        };

        $scope.initSubscriptionDetail = function () {
            companyData.updateSubscriptionDetail().then(function (resp) {
            })
            .catch(function () {

            });
        };
        $scope.initMapping = function () {
            angular.forEach($scope.model.subscriptionList, function (item) {
                item.mapping = false;
            });
        },
        $scope.getFirstTimeSubscriptions = function () {
            //check if database is empty only then call get all subscriptions
            companyData.checkDatabase().then(function (resp) {
                var response = resp.data;
                if(response=="True")
                {
                    $scope.initSubscriptionDetail();
                }
            })
            .catch(function () {

            });
        },
        $scope.saveCookie = function () {
           var name =  $.cookie("CompanyName", $scope.model.filter.companyName, { path: '/', expires: 1 });
        },
        $scope.getOnlyCompanyUsers = function () {
            
        }
    }



})();