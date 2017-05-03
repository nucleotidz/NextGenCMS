(function () {
    'use strict';
    app.controller('WorkflowDetailsController', ['$scope', 'WorkFlowAPI', '$q', '$stateParams', '$state', 'Global',
    function ($scope, WorkFlowAPI, $q, $stateParams, $state, Global) {
        var vm = this;
        var workflowInstanceId = $stateParams.WorkFlowID;
        vm.taskdata = [];
        vm.WfModel = {
            title: "",
            description: "",
            createdBy: "",
            duedate: "",
            status: "",
            message: "",
            fileName: "",
            FileId: "",
            endDate: "",
            startDate: "",
            priority: ""
        }
        vm.taskDatasource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success(vm.taskdata);
                }
            },
            pageSize: 1000,
            sort: {
                field: "properties.cm_created",
                dir: "desc"
            },
            schema: {
                model: {
                    action: "",
                    fields: {
                        "title": {
                            type: "string", editable: false
                        },
                        "owner.userName": {
                            type: "string", editable: false
                        },
                        "properties.cm_created": {
                            type: "date", editable: false
                        },
                        "properties.bpm_status": {
                            type: "string", editable: false
                        },
                        "properties.bpm_comment": {
                            type: "string", editable: false
                        }
                    }
                }
            },
        });

        vm.taskGridOptions = {
            dataSource: vm.taskDatasource,
            dataBound: function () {
            },
            sortable: true,
            reorderable: true,
            resizable: true,
            navigatable: true,
            scrollable: true,
            height: 150,
            selectable: "multiple",
            columns: [
            {
                field: "title", title: "Type"
            },
            {
                field: "owner.userName", title: "Assigned To"
            },
            {
                field: "properties.cm_created", title: "Created Date", template: "#= kendo.toString(kendo.parseDate(properties.cm_created), 'dd MMM yyyy hh:mm:ss') #",
            },
            {
                field: "properties.bpm_status", title: "Status"
            },
             {
                 field: "properties.bpm_comment", title: "Comment"
             }
            ]
        };
        $scope.DownloadFile = function (id) {
            var formId = "formFile";
            angular.element("body").append("<form  method='POST' id='" + formId + "' action='" + Global.apiuri + "File/Download/By/Id" + "' target='_tab' >");
            $("#" + formId + "").append("<input type='hidden' value='" + id + "'  name='id' >");
            angular.element("#" + formId + "").submit();
            angular.element("#" + formId + "").remove();
        }

        function init() {
             $(".loader").show();
            var data = WorkFlowAPI.GetWorkflowDetails({ 'wfid': workflowInstanceId });
            var data1 = WorkFlowAPI.GetWorkFlowFile({ "Id": workflowInstanceId.split("$")[1] });
            $q.all([data.$promise, data1.$promise]).then(function (response) {
                var WFDATa = response[0].data;              
                vm.taskdata = WFDATa.tasks;
                vm.taskDatasource.read();
                vm.WfModel.title = WFDATa.title;
                vm.WfModel.description = WFDATa.description;
                vm.WfModel.createdBy = WFDATa.initiator.firstName + ' ' + WFDATa.initiator.lastName;
                vm.WfModel.duedate = kendo.toString(kendo.parseDate(WFDATa.dueDate), 'dd MMM yyyy')
                vm.WfModel.status = WFDATa.isActive === true ? "Workflow is in progress" : "Completed";
                vm.WfModel.message = WFDATa.message;
                vm.WfModel.startDate = kendo.toString(kendo.parseDate(WFDATa.startDate), 'dd MMM yyyy hh:mm:ss');
                vm.WfModel.priority = WFDATa.priority === "1" ? "High" : (WFDATa.priority === "2" ? "Medium" : "Low");
                if (response[1].list != undefined && response[1].list.entries.length > 0) {
                    var fileData = response[1].list.entries[0].entry;
                    vm.WfModel.fileName = fileData.name;
                    vm.WfModel.FileId = fileData.id;
                }
                vm.WfModel.endDate = WFDATa.isActive === true ? "<In Progress>" : kendo.toString(kendo.parseDate(WFDATa.endDate), 'dd MMM yyyy');

                $(".loader").hide();
            });
        };
        init();

    }]);
})();