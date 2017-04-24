(function () {
    'use strict';
    app.controller('DeleteUserController', ['$scope', '$modalInstance', 'items',
function ($scope, $modalInstance, items) {
    $scope.message = "";
    $scope.yes = function () {
        //var users = items.users;
        //var data = AdministrationApi.deleteUsers(users);
        //$q.all([data.$promise]).then(function (response) {
        //if (response[0].overallSuccess) {
        $modalInstance.dismiss("yes");
        //}
        //});
    };

    $scope.no = function () {
        $modalInstance.dismiss("no");
    };

    function bind() {
        $scope.message = items;
    }
    bind();
}])
})();