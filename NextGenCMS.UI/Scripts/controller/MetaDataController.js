(function () {
    'use strict';
    app.controller('MetaDataController', ['$scope', '$stateParams', '$state',
    function ($scope, $stateParams, $state) {
        var vm = this;
        $scope.meta = $stateParams.fileMeta;
    }])
})();
