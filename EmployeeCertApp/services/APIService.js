
app.factory("APIFactory", function ($http)
{
    var certAppFactory = {};
    var baseaddress = 'http://192.168.97.194//EmployeeCertApp_WebAPI/';

    var _getCategoryDetails = function ()
    {
        return $http.get(
                    baseaddress + 'api/certification/getCertificationCategories/', { headers: { 'Content-Type': 'application/json' } }).
            then(function (response) {
                return response;
            });
 
    }

    var _postCertification = function (certification)
    {
        return $http.post(
                    baseaddress + 'api/certification/postCertification/', certification, { headers: { 'Content-Type': 'application/json' } }).
            then(function (response) {
                return response;
            });
    }


    var _getCertifications = function () {
        return $http.get(baseaddress + 'api/certificate/getCertificationList/', { headers: { 'Content-Type': 'application/json' } }).
            then(function (response) {
                return response;
            });
    }

    var _updateCertification = function (certification) {
        return $http.put(baseaddress + 'api/certificate/updateCertificate/',certification, { headers: { 'Content-Type': 'application/json' } }).
            then(function (response) {
                return response;
            });
    }

    //add methods to factory
    certAppFactory = {
        getCategoryDetails: _getCategoryDetails,
        postCertification: _postCertification,
        getCertifications: _getCertifications,
        updateCertification: _updateCertification
    };

        return certAppFactory;
    
});