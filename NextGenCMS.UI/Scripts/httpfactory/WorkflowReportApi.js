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
                method: "GET",
                url: Global.apiuri + "report/workflow/active/:username",
                isArray: false,
                params: {}
            },
            getCompletedWorkflows: {
                method: "GET",
                url: Global.apiuri + "report/workflow/completed/:username",
                isArray: false
            },
            getWorkflowsDueToday: {
                method: "GET",
                url: Global.apiuri + "report/workflow/due/today/:username",
                isArray: false
            },
            getWorkflowsDueTomorrow: {
                method: "GET",
                url: Global.apiuri + "report/workflow/due/tomorrow/:username",
                isArray: false
            },
            getWorkflowsDueNext7Days: {
                method: "GET",
                url: Global.apiuri + "report/workflow/due/next7days/:username",
                isArray: false
            },
            getWorkflowsOverdue: {
                method: "GET",
                url: Global.apiuri + "report/workflow/overdue/:username",
                isArray: false
            },
            getWorkflowsNoDueDate: {
                method: "GET",
                url: Global.apiuri + "report/workflow/noduedate/:username",
                isArray: false
            },
            getWorkflowsStartedinLast7days: {
                method: "GET",
                url: Global.apiuri + "report/workflow/started/last7days/:username",
                isArray: false
            },
            getWorkflowsStartedinLast14days: {
                method: "GET",
                url: Global.apiuri + "report/workflow/started/last14days/:username",
                isArray: false
            },
            getWorkflowsStartedinLast28days: {
                method: "GET",
                url: Global.apiuri + "report/workflow/started/last28days/:username",
                isArray: false
            },
            getWorkflowsHighPriority: {
                method: "GET",
                url: Global.apiuri + "report/workflow/priority/high/:username",
                isArray: false
            },
            getWorkflowsMediumPriority: {
                method: "GET",
                url: Global.apiuri + "report/workflow/priority/medium/:username",
                isArray: false
            },
            getWorkflowsLowPriority: {
                method: "GET",
                url: Global.apiuri + "report/workflow/priority/low/:username",
                isArray: false
            }
        });
    }]);
})();
