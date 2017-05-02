(function () {
    'use strict';
    app.controller('DashboardController', ['$scope', 'WorkFlowAPI', '$q',
    function ($scope, WorkFlowAPI,$q) {
        var vm = this;
        var tasks = WorkFlowAPI.GetWorkFlow();
        $q.all([tasks.$promise]).then(function (response) {
            if (response.length > 0) {
                vm.taskCount = response[0].length;
            }
        });
    }]);
})();