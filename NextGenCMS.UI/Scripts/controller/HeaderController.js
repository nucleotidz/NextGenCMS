(function () {
    'use strict';
    app.controller('HeaderController', ['$scope', 'Cache','WorkFlowAPI','$q',
    function ($scope, Cache, WorkFlowAPI, $q) {
        var vm = this;
        var displayName = Cache.get('displayName');
        vm.Name = displayName;

        var tasks = WorkFlowAPI.GetWorkFlow();
        $q.all([tasks.$promise]).then(function (response) {
            if (response.length > 0) {
                vm.taskCount = response[0].length;
            }
        });
    }]);
})();
