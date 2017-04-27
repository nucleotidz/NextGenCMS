(function () {
    'use strict';
    app.controller('WorkflowDetailsController', ['$scope',  'WorkFlowAPI', 'AdministrationApi', '$q', 'Profile', 
    function ($scope, $modalInstance, WorkFlowAPI, AdministrationApi, $q, Profile) {
        vm = this;
        var workflowInstanceId = 'activiti$3101'
        vm.taskDatasource = new kendo.data.DataSource({
            schema: {
                model: {                  
                    fields: {
                        description: { type: "string" },
                        title: { type: "string" },
                        "properties.bpm_status": { type: "number" },
                        "owner.userName": { type: "string" },
                        "properties.cm_created": { type: "string" }                      
                    }
                }
            }
        });
        function init() {
            var data = WorkFlowAPI.GetWorkflowDetails({ 'wfid': workflowInstanceId });

        };     
        init();
    }]);
})();