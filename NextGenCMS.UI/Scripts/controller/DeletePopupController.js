(function () {
    'use strict';
    app.controller('DeletePopupController', ['$scope', '$modalInstance', 'items',
function ($scope, $modalInstance, items) {
    $scope.message = "";
    $scope.yes = function () {
        $modalInstance.dismiss("yes");
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