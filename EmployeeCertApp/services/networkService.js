/// <reference path="../scripts/angular.js" />
app.factory('networkService', function ($http) {

    var networkServiceFactory = {};




    var updateEmpDetails = function (empId, empDetails) {
        return $http.put(
            'http://192.168.97.194/EmployeeCertApp_WebAPI/' + 'api/register/updateEmployeeDetails/' + empId, empDetails, {
                header: {
                    'Content-Type': 'application/json'
                }
            }).
        then(function (response) {
            return response;
        });
    };


    var sendEmpDetails = function (empDetails) {
        return $http.post(
            'http://192.168.97.194/EmployeeCertApp_WebAPI/' + 'api/register/postEmployeeDetails/', empDetails, {
                header: {
                    'Content-Type': 'application/json'
                }
            }).
        then(function (response) {
            return response;
        });
    };


    var getEmpDetails = function (empId) {
        return $http.get(
           'http://192.168.97.194/EmployeeCertApp_WebAPI/' + 'api/employee/GetEmployeeDetailsPerEmpId/' + empId, {
               headers: {
                   'Content-Type': 'application/json'
               }
           }).
       then(function (response) {
           return response;
       });
    };


    //for getting designation for employeemaster
    var getDesignation = function () {
        return $http.get(
            'http://192.168.97.194/EmployeeCertApp_WebAPI/' + 'api/designation/getAllDesignation/', {
                header: {
                    'Content-Type': 'application/json'
                }
            }).
        then(function (response) {
            return response;
        });

    };
    //for getting technology for employeemaster
    var getTechnology = function () {
        return $http.get(
            'http://192.168.97.194/EmployeeCertApp_WebAPI/' + 'api/certificate/getCertificateCategories/', {
                header: {
                    'Content-Type': 'application/json'
                }
            }).
        then(function (response) {
            return response;
        });

    };


    networkServiceFactory = {
        sendEmpDetails: sendEmpDetails
        , getDesignation: getDesignation
        , getTechnology: getTechnology
        , getEmpDetails: getEmpDetails
        , updateEmpDetails: updateEmpDetails

    };

    return networkServiceFactory;
});