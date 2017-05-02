(function () {
    'use strict';
    app.controller('WorkflowReportController', ['$scope', 'WorkflowReportApi', '$q',
    function ($scope, WorkflowReportApi, $q) {
        var vm = this;
        vm.userName = "all";
        vm.groupDataSource = new kendo.data.DataSource();
        vm.WorkflowData = {
            message: "",
            title: "",
            description: "",
            username: "",
            dueDate: "",
            endDate: "",
            startDate: "",
            priority: "",
            status: ""
        };
        vm.header = "Active Workflows";
        vm.noWorkflows = "No workflows";
        vm.noRecordExist = false;
        function init() {
            vm.getActiveWorkflows();
        };

        vm.getActiveWorkflows = function () {
            vm.header = "Active Workflows";
            var data = WorkflowReportApi.getActiveWorkflows({ "username": vm.userName });
            vm.loadWorkflows(data);
        };

        vm.getCompletedWorkflows = function () {
            vm.header = "Completed Workflows";
            var data = WorkflowReportApi.getCompletedWorkflows({ "username": vm.userName });
            vm.loadWorkflows(data);
        };

        vm.getWorkflowsDueToday = function () {
            vm.header = "Workflows Due Today";
            var data = WorkflowReportApi.getWorkflowsDueToday({ "username": vm.userName });
            vm.loadWorkflows(data);
        };

        vm.getWorkflowsDueTomorrow = function () {
            vm.header = "Workflows Due Tomorrow";
            var data = WorkflowReportApi.getWorkflowsDueTomorrow({ "username": vm.userName });
            vm.loadWorkflows(data);
        };

        vm.getWorkflowsDueNext7Days = function () {
            vm.header = "Workflows Due in Next 7 Days";
            var data = WorkflowReportApi.getWorkflowsDueNext7Days({ "username": vm.userName });
            vm.loadWorkflows(data);
        };

        vm.getWorkflowsOverdue = function () {
            vm.header = "Overdue Workflows";
            var data = WorkflowReportApi.getWorkflowsOverdue({ "username": vm.userName });
            vm.loadWorkflows(data);
        };

        vm.getWorkflowsNoDueDate = function () {
            vm.header = "Workflows Without a Due Date";
            var data = WorkflowReportApi.getWorkflowsNoDueDate({ "username": vm.userName });
            vm.loadWorkflows(data);
        };

        vm.getWorkflowsStartedinLast7days = function () {
            vm.header = "Workflows Started in Last 7 days";
            var data = WorkflowReportApi.getWorkflowsStartedinLast7days({ "username": vm.userName });
            vm.loadWorkflows(data);
        };

        vm.getWorkflowsStartedinLast14days = function () {
            vm.header = "Workflows Started in Last 14 days";
            var data = WorkflowReportApi.getWorkflowsStartedinLast14days({ "username": vm.userName });
            vm.loadWorkflows(data);
        };

        vm.getWorkflowsStartedinLast28days = function () {
            vm.header = "Workflows Started in Last 28 days";
            var data = WorkflowReportApi.getWorkflowsStartedinLast28days({ "username": vm.userName });
            vm.loadWorkflows(data);
        };

        vm.getWorkflowsHighPriority = function () {
            vm.header = "High Priority Workflows";
            var data = WorkflowReportApi.getWorkflowsHighPriority({ "username": vm.userName });
            vm.loadWorkflows(data);
        };

        vm.getWorkflowsMediumPriority = function () {
            vm.header = "Medium Priority Workflows";
            var data = WorkflowReportApi.getWorkflowsMediumPriority({ "username": vm.userName });
            vm.loadWorkflows(data);
        };

        vm.getWorkflowsLowPriority = function () {
            vm.header = "Low Priority Workflows";
            var data = WorkflowReportApi.getWorkflowsLowPriority({ "username": vm.userName });
            vm.loadWorkflows(data);
        };

        vm.loadWorkflows = function (data) {
            $scope.source = new kendo.data.DataSource({
                data: [],
                pageSize: 21
            });
            vm.noRecordExist = false;
            $q.all([data.$promise]).then(function (response) {
                if (response != null && response != undefined && response.length > 0 && response[0].data != null && response[0].data.length > 0) {
                    vm.listData = [];
                    angular.forEach(response[0].data, function (data) {
                        vm.WorkflowData = {};
                        vm.WorkflowData.message = data.message;
                        vm.WorkflowData.title = data.title;
                        vm.WorkflowData.description = data.description;
                        var lastName = data.initiator.lastName;
                        vm.WorkflowData.username = data.initiator.firstName + (lastName === null && lastName === "" ? "" : " " + lastName);
                        vm.WorkflowData.dueDate = kendo.toString(kendo.parseDate(data.dueDate), 'dd MMM yyyy');
                        vm.WorkflowData.endDate = data.endDate === null || data.endDate === "" ? "<in progress>" : kendo.toString(kendo.parseDate(data.endDate), 'dd MMM yyyy hh:mm:ss');
                        vm.WorkflowData.startDate = kendo.toString(kendo.parseDate(data.startDate), 'dd MMM yyyy hh:mm:ss');
                        vm.WorkflowData.priority = data.priority === "1" ? "High" : (data.priority === "2" ? "Medium" : "Low");
                        vm.WorkflowData.status = data.isActive ? "Workflow is in Progress" : "Workflow is Complete";
                        vm.listData.push(vm.WorkflowData);
                    });
                    //vm.workflowGridDataSource.read();

                    $scope.source = new kendo.data.DataSource({
                        data: vm.listData,
                        pageSize: 21
                    });
                }
                else {
                    vm.noRecordExist = true;
                }
            });
        }
        //vm.workflowGridDataSource = new kendo.data.DataSource({
        //    type: "json",
        //    transport: {
        //        read: function (o) {
        //            o.success(vm.WorkflowData);
        //        }
        //    },
        //    pageSize: 1000,
        //    schema: {
        //        model: {
        //            action: "",
        //            fields: {
        //                title: {
        //                    type: "string", editable: false
        //                },
        //                description: {
        //                    type: "string", editable: false
        //                },
        //                message: {
        //                    type: "string", editable: false
        //                },
        //                //initiator: {
        //                //    type: "string", editable: false
        //                //},
        //                startDate: {
        //                    type: "date", editable: false
        //                },
        //                dueDate: {
        //                    type: "date", editable: false
        //                },
        //                endDate: {
        //                    type: "date", editable: false
        //                }
        //            }
        //        }
        //    },
        //});

        //vm.workflowGridOptions = {
        //    dataSource: vm.workflowGridDataSource,
        //    dataBound: function () {
        //    },
        //    sortable: {
        //        mode: "multiple",
        //        allowUnsort: true
        //    },
        //    reorderable: true,
        //    resizable: true,
        //    navigatable: true,
        //    scrollable: {
        //        virtual: true
        //    },
        //    height: 200,
        //    selectable: "multiple",
        //    pageable: {
        //        numeric: false,
        //        previousNext: false,
        //        messages: {
        //            empty: "No Records exist",
        //            display: "No of records is: {2}"
        //        }
        //    },
        //    columns: [
        //    {
        //        field: "title", title: "Title"
        //    },
        //    {
        //        field: "description", title: "Description"
        //    },
        //    {
        //        field: "message", title: "Message"
        //    },
        //    //{
        //    //    field: "initiator.username", title: "Initiator"
        //    //},
        //    {
        //        field: "startDate", title: "Start Date", format: "{0:dd-MM-yyyy}"
        //    },
        //    {
        //        field: "dueDate", title: "Due Date", format: "{0:dd-MM-yyyy}"
        //    },
        //    {
        //        field: "endDate", title: "End Date", format: "{0:dd-MM-yyyy}"
        //    }
        //    ]
        //};

        init();
    }]);
})();