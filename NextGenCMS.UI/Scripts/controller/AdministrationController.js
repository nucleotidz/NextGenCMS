(function () {
    'use strict';
    app.controller('AdministrationController', ['$scope', '$state', '$modal',
    function ($scope, $state, $modal) {
        var vm = this;
        //var token = Cache.get('token');
        //vm.addUser = function () {
        //    $http({
        //        method: 'GET',
        //        url: 'http://127.0.0.1:8080/alfresco/s/api/people-enterprise?alf_ticket=' + token,

        //    }).then(function successCallback(response) {
        //        vm.Documents = response.data.items;
        //    }, function errorCallback(response) {
        //        var l = 0;
        //    });
        //};
        vm.addUser = function () {
            var modalInstance = $modal.open({
                backdrop: 'static',
                keyboard: false,
                templateUrl: './Administration/AddUserPopup',
                controller: 'AddUserPopupController'
            });
            modalInstance.result.then(function () {
            }, function (popupData) {
                if (popupData !== "close") {
                    alert(popupData);
                }
            });
        };

        vm.addGroup = function () {

        };
        vm.goToUser = function () {
            $state.go("Home.User");
        };

        vm.goToGroup = function () {
            $state.go("Home.Group");
        };
    }]);
})();
