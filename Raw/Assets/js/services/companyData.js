(function () {
    'use strict';
    app.factory("companyData", companyData);
    companyData.$inject = ["$http", "$q", "BASE_URL"];
    function companyData($http, $q, BASE_URL) {
        return {
            addEndUserMapping: function (filter) {
                return $http.post(BASE_URL + 'company/addEndUserMapping', filter).then(function (data) {
                    return data;
                });
            }, upsertCompanyOrderMapping: function (companyOrder) {
                return $http.post(BASE_URL + 'company/upsertCompanyOrderMapping', companyOrder).then(function (data) {
                    return data;
                });
            }, getCompanyEndUsers: function (filter) {
                return $http.post(BASE_URL + 'company/getCompanyEndUsers', filter).then(function (data) {
                    return data;
                });
            }, getCompanyOrders: function (filter) {
                return $http.post(BASE_URL + 'company/getCompanyOrders', filter).then(function (data) {
                    return data;
                });
            }, deleteEndUserMapping: function (user,customerIds) {
                return $http.post(BASE_URL + 'company/deleteEndUserMapping', { user: user, customerIds: customerIds }).then(function (data) {
                    return data;
                });
            }, deleteCompanyOrderMapping: function (recordIds) {
                return $http.post(BASE_URL + 'company/deleteCompanyOrderMapping', recordIds).then(function (data) {
                    return data;
                });
            }, refreshCompanies: function () {
                return $http.post(BASE_URL + 'company/refreshCompanyList').then(function (data) {
                    return data;
                });
            }
        };
    };
})();
