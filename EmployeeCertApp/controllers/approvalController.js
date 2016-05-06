app.controller('approvalController', function (httpService, $scope, $modal) {
    $scope.showDetails = false;

    $scope.sortColumn = "name";

    $scope.reverseSort = false;

    $scope.getSortClass = function (column) {
        if ($scope.sortColumn == column) {
            return $scope.reverseSort ? 'arrow-down' : 'arrow-up';
        }
        else
            return '';
    }

    $scope.getSortOrder = function (column) {
        if ($scope.sortColumn == column) {
            $scope.reverseSort = !$scope.reverseSort;
            $scope.sortColumn = column;
        }
        else {
            $scope.reverseSort = false;
            $scope.sortColumn = column;
        }
    }

    httpService.getApprovalList().then(function (response) {
        if(response.data != null)
         $scope.approvalList = response.data;
    });

    $scope.openPopUp = function(index)
    {
        var modalInstance = $modal.open({
            backdrop: true,
            backdropClick: true,
            dialogFade: false,
            keyboard: true,
            templateUrl: 'views/certApprovalDetailscontroller.html',
            controller: 'certApprovalDetailsController',
            resolve: {
                modalData: function () {
                    return {
                        requestItem : $scope.approvalList[index]
                    }
                }
            }
        });

        modalInstance.result.then(function (data) {
            console.log(data);
        });
    }
   
});