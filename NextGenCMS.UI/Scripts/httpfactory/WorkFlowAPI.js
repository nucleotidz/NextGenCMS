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
            }
        });
    }]);
})();
