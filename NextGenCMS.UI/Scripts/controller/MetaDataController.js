(function () {
    'use strict';
    app.controller('MetaDataController', ['$scope', '$stateParams', '$state','$modalInstance', 'items',
    function ($scope, $stateParams, $state, $modalInstance, items) {
       // var vm = this;
        $scope.meta = items;
        $scope.closePopup = function () {
            $modalInstance.dismiss("close");
        }
    }])
})();
