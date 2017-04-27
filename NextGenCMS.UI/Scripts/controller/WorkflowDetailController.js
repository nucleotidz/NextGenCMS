(function () {
    'use strict';
    app.controller('WorkflowDetailsController', ['$scope', 'WorkFlowAPI', '$q',
    function ($scope, WorkFlowAPI, $q) {
        var vm = this;
        var workflowInstanceId = 'activiti$3101';          
        vm.taskdata = [];
       
        function init() {        
            var data = WorkFlowAPI.GetWorkflowDetails({ 'wfid': workflowInstanceId });
            $q.all([data.$promise]).then(function (response) {
                vm.taskdata = response[0].data.tasks;
                vm.taskDatasource = new kendo.data.DataSource({
                    data: vm.taskdata,
                    pageSize: 21   
                });
            });
        };
        init();
    }]);
})();