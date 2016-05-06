app.controller('loginController', function ($scope, httpService, $location, localStorageService) {
    $scope.onLogin = function () {
        if ($scope.loginForm.$valid) {
            if ($scope.userDetail.loginName !== null && $scope.userDetail.password !== null) {
                httpService.getUserDetails($scope.userDetail.loginName, $scope.userDetail.password).then(function (response) {
                        if (response.data !== "error")
                        {
                            if (response.data.EmployeeId)
                                localStorageService.set('EmployeeId', response.data.EmployeeId);
                                localStorageService.set('EmployeeCode', response.data.EmployeeCode);
                                localStorageService.set('userId', response.data.UserId);
                                httpService.getEmployeeName(response.data.EmployeeId).then(function (response) {
                                    if(response.data != null)
                                        localStorageService.set('EmpName', response.data.FirstName);
                                        $location.path('/dashBoard');
                                });
                            
                           
                        }
                        else {
                            $scope.message = "Invalid userid or password !!";
                        }
                    });
                }
        }
        else {
            $scope.message = "Please enter required fields. ";
        }
    }

    $scope.onCancel = function () {
        $scope.userDetail = null;
    }
    $scope.onRegister = function () {
        $location.path('/newUser');
    }
});