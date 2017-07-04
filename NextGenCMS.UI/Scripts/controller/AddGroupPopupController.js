(function () {
    'use strict';
    app.controller('AddGroupPopupController', ['$scope', '$modalInstance', 'AdministrationApi', '$q', 'items',
    function ($scope, $modalInstance, AdministrationApi, $q, items) {
        $scope.group = {
            fullName: "",           //mandatory - group's identifier, once set can not be changed
            displayName: "",         // mandatory - group's display name
            editMode: false
        };

        function init() {
            $scope.group = items === undefined ? {} : items;
        }

        $scope.createGroup = function () {
            $(".loader").show();
            if (validateForm()) {
                var data = AdministrationApi.createGroup($scope.group);
                $q.all([data.$promise]).then(function (response) {
                    if (response !== null && response.length === 1) {
                        var result = response[0];
                        if (result.status === 200) {
                            $modalInstance.dismiss("Group created successfully.");
                        }
                        else {
                            alert("Group '" + $scope.group.fullName + "' already exists.");
                        }
                    }
                    $(".loader").hide();
                });
            }
            else $(".loader").hide();
        };

        $scope.updateGroup = function () {
            $(".loader").show();
            if (validateForm()) {
                var data = AdministrationApi.updateGroup($scope.group);
                $q.all([data.$promise]).then(function (response) {
                    if (response !== null && response.length === 1) {
                        $modalInstance.dismiss("Group updated successfully.");
                    }
                    $(".loader").hide();
                });
            }
            else $(".loader").hide();
        };

        $scope.closePopup = function () {
            $modalInstance.dismiss("close");
        };

        function validateForm() {
            if ($scope.group.fullName === undefined || $scope.group.fullName === "") {
                alert("Identifier is required.");
                return false;
            }
            if ($scope.group.displayName === undefined || $scope.group.displayName === "") {
                alert("Display name is required.");
                return false;
            }
            return true;
        }

        init();
    }]);
})();