app.controller('nominationController', function ($scope, httpService, localStorageService, $q, $rootScope, $location) {

    $scope.getCertificationListPerSkills = getCertificationListPerSkills;
    init();
    function init()
    {
        //model for view
        $scope.request = {};
        $scope.request.amount = 0;
        // variable to pass the data from dashboard to this
        var selectedItem = localStorageService.get('selectedCertificationItem');
        // Gets the list of certification from tables/Db based
        httpService.getCertificationCategories().then(function (response) {
            if (response.data != null) {
                $scope.certificationCategoryList = response.data;
            }
            if(selectedItem)
            {
                var deferred = $q.defer();
                $scope.request.certificationCategory = selectedItem.CertificationCategoryId;
                getCertificationListPerSkills(deferred);
                $scope.request.RequestForCertificationId = selectedItem.CertificationId;
                    deferred.resolve(true);
                return deferred.promise;
            }
        });
    }
    $scope.onCancel = function()
    {
        $scope.request = null;
        $scope.successMsg = null;
        $scope.message = null;
        $scope.nominationForm.$submitted = false;
    }
    $scope.onNominate = function ()
    {
        if ($scope.nominationForm.$valid)
        {
            $scope.request.RequestByUserId = localStorageService.get('userId');
            httpService.nominate($scope.request).then(function (response) {
                $scope.successMsg = "Nomination has been done successfully."
                $scope.message = null;
                $scope.request = null;
                $scope.nominationForm.$submitted = false;
            });
        }
        else
        {
            $scope.message = "Please enter required details."
        }
       
    }
    function getCertificationListPerSkills(deferred)
    {
        httpService.getCertificationListPerSkills($scope.request.certificationCategory).then(function (response) {
            if(response.data != null)
            {
                $scope.certificationList = response.data;
                deferred.resolve(true);
            }
        });
    }
    $rootScope.$on("requestThroughNavigation", function () {
        localStorageService.remove('selectedCertificationItem');
        $scope.successMsg = null;
        $scope.message = null;
        $scope.nominationForm.$submitted = false;
        init();
    });
});