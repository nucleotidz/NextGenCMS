(function () {
    'use strict'
    app.factory('WorkFlowAPI', ['$resource', 'Global', function ($resource, Global) {
        return $resource('', {}, {
            GetWorkFlow: {
                method: "GET",
                url: Global.apiuri + "WorkFlow/Get",
                isArray: true                
            },
            CreateWorkflow: {
                method: "POST",
                url: Global.apiuri + "WorkFlow/Workflow",
                isArray: false,
                params: {}
            },
            GetWorkFlowFile: {
            method: "GET",
            url: Global.apiuri + "WorkFlow/Get/File/:Id",
            isArray: false                
            },
            UpdateWf: {
                method: "POST",
                url: Global.apiuri + "workflow/Update/Workflow/Activity",
                isArray: false                
            },
            ApproveReject: {
                method: "POST",
                url: Global.apiuri + "workflow/Action",
                isArray: false
            },
            DoneTask: {
                method: "POST",
                url: Global.apiuri + "workflow/Done",
                isArray: false
            },
            GetAllTasks:{
                method: "GET",
                url: Global.apiuri + "workflow/All/Task/:wfid",
                isArray: true
            },
            GetWorkflowDetails: {
                method: "GET",
                url: Global.apiuri + "workflow/WorkflowDetails/:wfid",
                isArray: false
            }
        });
    }]);
})();
