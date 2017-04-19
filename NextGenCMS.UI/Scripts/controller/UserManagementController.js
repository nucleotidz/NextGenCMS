(function () {
    'use strict';
    app.controller('UserManagementController', ['$scope','$modal',
    function ($scope, $modal) {
        var vm = this;

        vm.addUser = function () {
            var modalInstance = $modal.open({
                backdrop: 'static',
                keyboard: false,
                templateUrl: './Administration/AddUserPopup',
                controller: 'AddUserPopupController'
            });
        };
    }]);
})();