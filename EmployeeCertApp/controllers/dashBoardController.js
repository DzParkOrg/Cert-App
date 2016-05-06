app.controller('dashBoardController', function ($scope, localStorageService, httpService, $location) {
    var empId = localStorageService.get('EmployeeId');
    if(empId != null)
    {
        httpService.getCertificationInfo(empId).then(function (response) {
          if (response.data != null)
          {
              $scope.certificateList = response.data;
          }
        });
    }
    $scope.nominate = function(index)
    {
        var selectedItem = $scope.certificateList[index];
        localStorageService.set('selectedCertificationItem', selectedItem);
        $location.path('/nomination');
    }
});