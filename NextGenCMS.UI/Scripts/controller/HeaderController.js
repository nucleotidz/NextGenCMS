(function () {
    'use strict';
    app.controller('HeaderController', ['$scope', 'Cache','WorkFlowAPI','$q','DataSharingService',
    function ($scope, Cache, WorkFlowAPI, $q, DataSharingService) {
        var vm = this;
        vm.data = DataSharingService.data;
        var displayName = Cache.get('displayName');
        vm.Name = displayName;
    }]);
})();
