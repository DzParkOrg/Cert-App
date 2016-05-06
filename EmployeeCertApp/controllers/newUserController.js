app.controller('newUserController', function ($scope, httpService, networkService, $location) {
    $scope.disableIndicator = true;
    $scope.onRegister = function () {
        if ($scope.registrationForm.$valid) {
            if ($scope.cnfrmPass !== $scope.userDetail.password) {
                $scope.message = "Password and confirm password values should be same.";
                $scope.registrationForm.password.$invalid = true;
                $scope.registrationForm.cnfrmPass.$invalid = true;
            } else {
                $scope.userDetail.employeeId = $scope.employee.EmployeeID;
                $scope.userDetail.createdByUserId = $scope.employee.EmployeeID;
                $scope.userDetail.LastModdifiedbyUserId = $scope.employee.EmployeeID;
                httpService.registerUser($scope.userDetail).then(function (response) {
                    if (response.data === "success") {
                        $scope.successMessage = "Registration done successfully.";
                        $location.path('/login');
                    } else if (response.data === "already exists") {
                        $scope.message = "It seems you are already registered with us.";
                    }
                });
            }
        } else {
            $scope.message = "Please enter all required fields.";
        }
    }
    $scope.onCancel = function () {
        $scope.userDetail = null;
        $location.path('/login');
    }
    $scope.getEmployeeDetails = function () {
        if ($scope.employeeCode) {

            httpService.getEmployeeDetails($scope.employeeCode).then(function (response) {
                console.log(response.data);
                if (response.data === "not exists") {
                    $scope.message = "Invalid employee code. Please enter valid employee code.";
                    $scope.employee = null;
                    $scope.disableIndicator = true;
                    $scope.userDetail = null;
                    $scope.cnfrmPass = null;
                } else {
                    $scope.employee = response.data;
                    networkService.getDesignation().then(function (response) {
                        //console.log(response.data);
                        angular.forEach(response.data, function (object, index) {
                            if ($scope.employee.DesignationId === object.DesignationID) {
                                $scope.designationTitle = object.DesignationTitle;
                            }
                            //console.log('ID'+object.DesignationID+'Nmae'+object.DesignationTitle);
                        });
                    }
                    );
                    $scope.Photo = $scope.employee.ProfilePhoto;
                    $scope.image = new Image();

                    if ($scope.Photo == null || $scope.Photo == '' || $scope.Photo == undefined) {
                        document.getElementById("profilePhoto").src = "css/images/profile.png";
                        if ($scope.image.src) {
                            // $scope.imageIndicator = true;
                        }
                    } else {
                        $scope.image.src = 'data:image/png;base64,' + $scope.Photo;
                        document.getElementById("profilePhoto").src = $scope.image.src;
                        if ($scope.image.src) {
                            // $scope.imageIndicator = true;
                        }
                    }
                    $scope.disableIndicator = false;
                    $scope.message = null;
                }
            });
        } else {
            $scope.message = "Please enter employee code.";
        }
    }
    $scope.uploadPicture = function () {
        $scpe.image = $scope.photo;
    }
});