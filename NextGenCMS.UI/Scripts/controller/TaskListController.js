(function () {
    'use strict';
    app.controller('TaskListController', ['$scope', 'WorkFlowAPI', '$q',
    function ($scope, WorkFlowAPI, $q) {
        var vm = this;
        var grddata = [];

        var data = WorkFlowAPI.GetWorkFlow();
        $q.all([data.$promise]).then(function (response) {
            grddata = response[0]
            vm.wfGridDataSource.read();
        });
        vm.wfGridDataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success(grddata);
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
                        firstName: {
                            type: "string", editable: false
                        },
                        state: {
                            type: "string", editable: false
                        },
                        startDate: {
                            type: "string", editable: false
                        },
                        dueDate: {
                            type: "string", editable: false
                        },
                        activityid: {
                            type: "string", editable: false
                        }

                    }
                }
            },
        });
        vm.wfGridOption = {
            dataSource: vm.wfGridDataSource,
            dataBound: function () {
            },
            sortable: {
                mode: "multiple",
                allowUnsort: true
            },
            reorderable: true,
            resizable: true,
            navigatable: true,
            scrollable: true,
            height: 300,
            selectable: "row",
            filterable: true,
            footer: false,
            columns: [
            {
                field: "title", title: "Title"
            },
            {
                field: "firstName", title: "Creator"
            },
            {
                field: "state", title: "State"
            },
            {
                field: "startDate", title: "Started On", template: "#= kendo.toString(kendo.parseDate(startDate), 'dd MMM yyyy') #"
            },
            {
                field: "dueDate", title: "Due", template: "#= kendo.toString(kendo.parseDate(dueDate), 'dd MMM yyyy') #"
            },
            {
                field: "activityid", title: "ActivityId", hidden: true
            }
            ]
        }

    }]);
})();