app.controller('navController', function ($scope, $location, localStorageService, networkService, httpService, $rootScope) {
    init();
    function init()
    {
        getEmployeeDetailsData(localStorageService.get('EmployeeId'));

        if($location.$$path == '/dashBoard')
        {
            $scope.pageName = 'Certification Info';
        }
        else if ($location.$$path == '/nomination')
        {
            $scope.pageName = 'Apply for Certification ';
        }
        else if ($location.$$path == '/approve') {
            $scope.pageName = 'Approve Certification Requests ';
        }
        else if ($location.$$path == '/employee') {
            $scope.pageName = 'Employee Master';
        } else if ($location.$$path == '/editEmployee') {
            $scope.pageName = 'Edit Employee details';
        }
        $scope.userName = localStorageService.get('EmpName');
    }

    function getEmployeeDetailsData(empId) {
       httpService.getEmployeeName(empId).then(function (data) {
           $scope.empMaster = data.data;
          
            $scope.Photo = $scope.empMaster.ProfilePhoto;
            
            $scope.image = new Image();
            if ($scope.Photo == null || $scope.Photo == '' || $scope.Photo == undefined) {
                document.getElementById("photo").src = "css/images/profile.png";
                if ($scope.image.src) {
                    $scope.imageIndicator = true;
                }
            } else {
                $scope.image.src = 'data:image/png;base64,' + $scope.Photo;
                document.getElementById("photo").src = $scope.image.src;
                if ($scope.image.src) {
                    $scope.imageIndicator = true;
                }
            }
            //$scope.image.src = 'data:image/png;base64,' + $scope.Photo;

            //document.getElementById("photo").src = $scope.image.src;
            //if ($scope.image.src)
            //    $scope.imageIndicator = true;
        });
    }


    $scope.getHomePage = function ()
    {
        $location.path('/dashBoard');
    }

    $scope.isActive = function (viewLocation) {
        //if (viewLocation === $location.path()) {
        //    return true;
        //}
        //else
        //    return false;
    };
    $scope.requestCertification = function () {
        $rootScope.$broadcast("requestThroughNavigation");
    }
    $scope.logOut = function()
    {
        localStorageService.clearAll();
    }
});