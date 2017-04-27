(function () {
    'use strict';
    app.controller('WorkflowReportController', ['$scope', 'WorkflowReportApi', '$q',
    function ($scope, WorkflowReportApi, $q) {
        var vm = this;
        vm.userName = "all";
        vm.groupDataSource = new kendo.data.DataSource();
        function init() {
            var data = WorkflowReportApi.getCompletedWorkflows({ "username": vm.userName });
            $q.all([data.$promise]).then(function (response) {
                vm.WorkflowData = response[0].data;
                //vm.workflowGridDataSource.read();

                $scope.source = new kendo.data.DataSource({
                    data: vm.WorkflowData,
                    pageSize: 21
                });
            });
        };

        vm.WorkflowData = [];
        vm.workflowGridDataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success(vm.WorkflowData);
                }
            },
            pageSize: 1000,
            schema: {
                model: {
                    action: "",
                    fields: {
                        title: {
                            type: "string", editable: false
                        },
                        description: {
                            type: "string", editable: false
                        },
                        message: {
                            type: "string", editable: false
                        },
                        //initiator: {
                        //    type: "string", editable: false
                        //},
                        startDate: {
                            type: "date", editable: false
                        },
                        dueDate: {
                            type: "date", editable: false
                        },
                        endDate: {
                            type: "date", editable: false
                        }
                    }
                }
            },
        });

        vm.workflowGridOptions = {
            dataSource: vm.workflowGridDataSource,
            dataBound: function () {
            },
            sortable: {
                mode: "multiple",
                allowUnsort: true
            },
            reorderable: true,
            resizable: true,
            navigatable: true,
            scrollable: {
                virtual: true
            },
            height: 200,
            selectable: "multiple",
            pageable: {
                numeric: false,
                previousNext: false,
                messages: {
                    empty: "No Records exist",
                    display: "No of records is: {2}"
                }
            },
            columns: [
            {
                field: "title", title: "Title"
            },
            {
                field: "description", title: "Description"
            },
            {
                field: "message", title: "Message"
            },
            //{
            //    field: "initiator.username", title: "Initiator"
            //},
            {
                field: "startDate", title: "Start Date", format: "{0:dd-MM-yyyy}"
            },
            {
                field: "dueDate", title: "Due Date", format: "{0:dd-MM-yyyy}"
            },
            {
                field: "endDate", title: "End Date", format: "{0:dd-MM-yyyy}"
            }
            ]
        };

        init();
    }]);
})();