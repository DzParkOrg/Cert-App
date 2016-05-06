/// <reference path="../scripts/angular.js" />
app.factory('httpService', function ($http) {
    

    var loginServiceFactory = {};
    var baseaddress = 'http://192.168.97.194//EmployeeCertApp_WebAPI/';
    var _getUserDetails = function (loginName, password) {
        return $http.get(
            baseaddress + 'api/login/getLoginDetails/' + loginName + '/' + password, { headers: { 'Content-Type': 'application/json' } }).
            then(function (response) {
            return response;
        });
    };

    var _registerUser = function (data) {
        return $http.post(
            baseaddress + 'api/login/registerUserDetails/', data, { headers: { 'Content-Type': 'application/json' } }).
            then(function (response) {
                return response;
            });
    };

    var _getEmployeeDetails = function (employeeCode) {
        return $http.get(
            baseaddress + 'api/register/getEmployeeDetails/' + employeeCode, { headers: { 'Content-Type': 'application/json' } }).
            then(function (response) {
                return response;
            });
    };

    var _getCertificationInfo = function (employeeId) {
        return $http.get(
            baseaddress + 'api/certificate/getCertification/' + employeeId, { headers: { 'Content-Type': 'application/json' } }).
            then(function (response) {
                return response;
            });
    };

    var _getCertificationCategories = function () {
        return $http.get(
            baseaddress + 'api/certificate/getCertificateCategories/', { headers: { 'Content-Type': 'application/json' } }).
            then(function (response) {
                return response;
            });
    };

    var _getCertificationListPerSkills = function (id) {
        return $http.get(
            baseaddress + 'api/certificate/getCertificationPerCategoryId/' + id, { headers: { 'Content-Type': 'application/json' } }).
            then(function (response) {
                return response;
            });
    };

    var _nominate = function (requestItem) {
        return $http.post(
            baseaddress + 'api/certificate/nomination/' , requestItem, { headers: { 'Content-Type': 'application/json' } }).
            then(function (response) {
                return response;
            });
    };

    var _getEmployeeName = function (employeeId) {
        return $http.get(
            baseaddress + 'api/employee/GetEmployeeDetailsPerEmpId/' + employeeId, { headers: { 'Content-Type': 'application/json' } }).
            then(function (response) {
                return response;
            });
    };

    var _getApprovalList = function () {
        return $http.get(
            baseaddress + 'api/certification/getApprovalList/' , { headers: { 'Content-Type': 'application/json' } }).
            then(function (response) {
                return response;
            });
    };


    var _updateCertificationStatus = function (id, request) {
        return $http.put(
            baseaddress + 'api/certification/updateCertificationStatus/' + id, request, {
                header: {
                    'Content-Type': 'application/json'
                }
            }).
        then(function (response) {
            return response;
        });
    };


    loginServiceFactory = {
        getUserDetails: _getUserDetails,
        registerUser: _registerUser,
        getEmployeeDetails: _getEmployeeDetails,
        getCertificationInfo: _getCertificationInfo,
        nominate: _nominate,
        getCertificationCategories: _getCertificationCategories,
        getCertificationListPerSkills: _getCertificationListPerSkills,
        getEmployeeName: _getEmployeeName,
        getApprovalList: _getApprovalList,
        updateCertificationStatus: _updateCertificationStatus
    };

    return loginServiceFactory;
});