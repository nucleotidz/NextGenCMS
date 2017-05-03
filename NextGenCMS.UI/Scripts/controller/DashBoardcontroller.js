(function () {
    'use strict';
    app.controller('DashboardController', ['$scope', 'WorkFlowAPI', '$q','DataSharingService',
    function ($scope, WorkFlowAPI, $q, DataSharingService) {
        var vm = this;
        vm.data = DataSharingService.data;
        var tasks = WorkFlowAPI.GetWorkFlow();
        $q.all([tasks.$promise]).then(function (response) {
            if (response.length > 0) {               
                DataSharingService.data.taskCount = response[0].length;
            }
        });
    }]);
})();