(function () {
    'use strict'
    app.factory('WorkflowReportApi', ['$resource', 'Global', function ($resource, Global) {
        return $resource('', {}, {
            getAllWorkflows: {
                method: "GET",
                url: Global.apiuri + "report/workflow/all/:username",
                isArray: true
            },
            getActiveWorkflows: {
                method: "POST",
                url: Global.apiuri + "report/workflow/active/:username",
                isArray: false,
                params: {}
            },
            getCompletedWorkflows: {
                method: "GET",
                url: Global.apiuri + "report/workflow/completed/:username",
                isArray: false
            }
        });
    }]);
})();
