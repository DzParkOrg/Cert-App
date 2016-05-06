app.controller('certApprovalDetailsController', function ($scope, $modalInstance, modalData, httpService) {
    init();
    function init()
    {
        $scope.request = {};
        $scope.request = modalData.requestItem;
    }
    $scope.closePopUp = function()
    {
        $modalInstance.close();
    }
    $scope.onCancel = function ()
    {
        $scope.request = {};
        $modalInstance.close();
    }
    $scope.onSubmit = function()
    {
        $scope.CertificationDetailsForm.$setSubmitted(true);
        if($scope.CertificationDetailsForm.$valid)
        {
            httpService.updateCertificationStatus($scope.request.RequestId , $scope.request).then(function (response) {
                if (response.data == "success")
                {
                    $modalInstance.close({
                        data: $scope.request.Status
                    });
                    
                }
            });
        }
        else
        {
            $scope.message = "Please enter required fields."
        }
    }
});