/// <reference path="Scripts/angular.js" />
/// <reference path="APIService.js" />

app.controller('CertCtrl', 
    function ($scope, APIFactory)
    {

        $scope.ErrMsg = '';
        $scope.mode = 'NEW';
        var selectedIndex = null;
        $scope.modificationDisabled = false;
        $scope.saveDisabled = false;
        $scope.certification = {};


    APIFactory.getCategoryDetails().then(function (response)
        {
            $scope.categories = response.data;
        }
    );


    $scope.add = function (mode)
    {
        //validate form
        if ($scope.frmCertificationMaster.$valid)
        {
            if ($scope.mode == "NEW")
            {
                //add to db via api
                APIFactory.postCertification($scope.certification).then(function (response) {
                    if (response.data == 'success') {
                        $scope.ErrMsg = 'Record created with efforts.';

                        APIFactory.getCertifications().then(function (response) {
                            $scope.certifications = response.data;
                        });

                    }
                    else {
                        $scope.ErrMsg = 'Record cannot be created due to some error.';
                    }
                });
            }
            else
            {
                var Id = $scope.certifications[selectedIndex].CertificationId;

                APIFactory.updateCertification($scope.certification).then(function (response) {
                    if (response.data == 'success') {
                        $scope.ErrMsg = 'Recored Updated.';

                        APIFactory.getCertifications().then(function (response) {
                            $scope.certifications = response.data;
                        });

                    }
                    else {
                        $scope.ErrMsg = 'Recored cannot be updated.';
                    }
                });
            }
        }
    }

    $scope.clear = function () {
        $scope.mode = "NEW";
    }

    $scope.select = function (index) {
        selectedIndex = index;
        $scope.mode = "EDIT";
        $scope.certification.CertificationId = $scope.certifications[index].CertificationId;
        $scope.certification.CertificationCode = $scope.certifications[index].CertificationCode;
        $scope.certification.CertificationTitle = $scope.certifications[index].CertificationTitle;
        $scope.certification.Description = $scope.certifications[index].Description;
        $scope.certification.CertificationCategoryId = $scope.certifications[index].CertificationCategoryId;
    }

    APIFactory.getCertifications().then(function (response)
    {
        $scope.certifications = response.data;
    });

    });
