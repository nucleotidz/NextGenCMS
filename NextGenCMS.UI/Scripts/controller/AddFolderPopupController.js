(function () {
    'use strict';
    app.controller('AddFolderPopupController', ['$scope', '$modalInstance', 'AdministrationApi', '$q',
    function ($scope, $modalInstance, AdministrationApi, $q) {
        $scope.FolderModel = {
            name: "",
            title: "",
            description: "",
            type: ""
        }
        $scope.createFolder = function () {
            $modalInstance.close($scope.FolderModel);
        };
        $scope.closePopup = function () {
            $modalInstance.dismiss("close");
        }
    }]);
})();