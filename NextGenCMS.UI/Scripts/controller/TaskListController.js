(function () {
    'use strict';
    app.controller('TaskListController', ['$scope', 'WorkFlowAPI', '$q', '$modal', 'DataSharingService',
    function ($scope, WorkFlowAPI, $q, $modal, DataSharingService) {
        var vm = this;
        var grddata = [];

        var data = WorkFlowAPI.GetWorkFlow();
        $q.all([data.$promise]).then(function (response) {
            grddata = response[0]
            vm.wfGridDataSource.read();
        });
        function Bind() {
            var data = WorkFlowAPI.GetWorkFlow();
            $q.all([data.$promise]).then(function (response) {
                grddata = response[0]
                vm.wfGridDataSource.read();
                DataSharingService.data.taskCount = response[0].length;
            });
        }
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
                        fullName: {
                            type: "string", editable: false
                        },
                        status: {
                            type: "string", editable: false
                        },
                        description: {
                            type: "string", editable: false
                        },
                        outcome: {
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
                        },

                        pid: {
                            type: "string", editable: false
                        },
                        comment: {
                            type: "string", editable: false
                        },
                        OwnerUsername: {
                            type: "string", editable: false
                        },
                        priority: {
                            type: "string", editable: false
                        },
                        taskId: {
                            type: "string", editable: false
                        },
                        workflowid: {
                            type: "string", editable: false
                        },


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
            toolbar: ["excel"],
            excel: {
                allPages: true
            },
            groupable: true,
            filterable: true,
            footer: false,
            columns: [
            {
                field: "title", title: "Title"
            },
            {
                field: "fullName", title: "Creator"
            },
            {
                field: "status", title: "State"
            },
             { field: "description", title: "description" },
            {
                field: "outcome", title: "Outcome"
            },
            {
                field: "startDate", title: "Started On", template: "#= kendo.toString(kendo.parseDate(startDate), 'dd MMM yyyy') #",
                groupHeaderTemplate: function (dataitem) {
                    return kendo.toString(kendo.parseDate(dataitem.value), 'dd MMM yyyy')
                }
            },
            {
                field: "dueDate", title: "Due", template: "#= kendo.toString(kendo.parseDate(dueDate), 'dd MMM yyyy') #",
                groupHeaderTemplate: function (dataitem) {
                    return kendo.toString(kendo.parseDate(dataitem.value), 'dd MMM yyyy')
                }
            },
            {
                field: "activityid", title: "ActivityId", hidden: true
            },
             {
                 field: "pid", title: "pid", hidden: true
             },
             {
                 field: "comment", title: "comment", hidden: true
             },
             {
                 field: "OwnerUsername", title: "OwnerUsername", hidden: true
             },
             {
                 field: "priority", title: "priority", hidden: true
             },
             {
                 field: "taskId", title: "taskId", hidden: true
             },
             { field: "workflowid", title: "workflowid", hidden: true },

            ]
        }
        vm.OpenDetails = function () {
            var entityGrid = $("#wf_grd").data("kendoGrid")
            var selectedItem = entityGrid.dataItem(entityGrid.select());

            var modalInstance = $modal.open({
                backdrop: 'static',
                keyboard: false,
                templateUrl: './Workflow/ViewEditWorkFlow',
                controller: 'ViewEditWfController',
                resolve: {
                    items: function () {
                        return selectedItem;
                    }
                }
            });
            modalInstance.result.then(function () {
            }, function (popupData) {
                if (popupData === "success") {
                    Bind();
                }
            });
        }
    }]);
})();