(function () {
    'use strict';
    app.controller('ViewEditWfController', ['$scope', '$rootScope', '$modalInstance', 'items', 'Global', '$timeout', 'WorkFlowAPI', '$q', '$http', 'Profile',
    function ($scope, $rootScope, $modalInstance, items, Global, $timeout, WorkFlowAPI, $q, $http, Profile) {
        var grddata = [];
        $scope.wf = {
            TaskEnable: false,
            ActionEnable: true,
            Owner: items.fullName,
            Priority: items.priority,
            DueDate: "",
            Status: "",
            StatusDataSource: [],
            disable: true,
            fileName: "",
            fileId: "",
            desc: items.description,
            comment: "",
            ActionOccured: true,
        }
        if (items.creatorUserName == Profile.get('Profile').User.userName) {
            $scope.wf.ActionOccured = false;           
          
            if (items.cm_name === "wf:approvedTask" || items.cm_name === "wf:rejectedTask") {
                  $scope.wf.TaskEnable = true;
            }
        }
        var ReassignModel = {
            taskId: '',
            username: '',
            IsResolve: '',
            comment: '',
            oldComment: items.comment,
            assigneeName: Profile.get('Profile').User.userName
            }
        $scope.wf.StatusDataSource.push({ "text": "Not Yet Started", "value": "Not Yet Started" })
        $scope.wf.StatusDataSource.push({ "text": "In Progress", "value": "In Progress" })
        $scope.wf.StatusDataSource.push({ "text": "On Hold", "value": "On Hold" })
        $scope.wf.StatusDataSource.push({ "text": "Cancelled", "value": "Cancelled" })
        $scope.wf.StatusDataSource.push({ "text": "Completed", "value": "Completed" })
        $scope.StatusOption = {
            dataSource: $scope.wf.StatusDataSource,
            dataTextField: "text",
            dataValueField: "value"
        };
        $timeout(function () {
            $scope.wf.Status = { "text": items.status, "value": items.status };
            $scope.wf.DueDate = items.dueDate
            $rootScope.$$phase = null
            $scope.$apply();
        }, 150);

        $scope.closePopup = function () {
            $modalInstance.dismiss("close");
        }
        function Bind() {
            var id = items.activityid.split("$")[1]
            var data = WorkFlowAPI.GetWorkFlowFile({ "Id": id });
            var allTaks = WorkFlowAPI.GetAllTasks({ "wfid": items.workflowid });
            $q.all([data.$promise, allTaks.$promise]).then(function (response) {
                if (response != undefined) {
                    grddata = response[1]
                    $scope.cGridDataSource.read();
                    $scope.wf.fileName = response[0].list.entries[0].entry.name
                    $scope.wf.fileId = response[0].list.entries[0].entry.id
                }
            });
        }
        $scope.DownloadFile = function (id) {
            var formId = "formFile";
            angular.element("body").append("<form  method='POST' id='" + formId + "' action='" + Global.apiuri + "File/Download/By/Id" + "' target='_tab' >");
            $("#" + formId + "").append("<input type='hidden' value='" + id + "'  name='id' >");
            $("#" + formId + "").append("<input type='hidden' value='" + tenant + "'  name='tenant' >");
            angular.element("#" + formId + "").submit();
            angular.element("#" + formId + "").remove();
        }
        $scope.Save = function () {
            var WFUpdate = [];
            if ($scope.wf.comment !== '') {
                WFUpdate.push({ "name": "bpm_comment", "value": $scope.wf.comment, "type": "d:text", "scope": "local" });
            }
            WFUpdate.push({ "name": "bpm_status", "value": $scope.wf.Status.value, "type": "d:text", "scope": "local" })
            var WFUpdateModel = {
                wfId: items.taskId,
                WFUpdate: WFUpdate
            }
            var WorkflowUpdateWrapper = {
                workflowModel: WFUpdateModel,
                oldComment: ReassignModel.oldComment,
                assignee: ReassignModel.assigneeName
            }
            $(".loader").show();
            var data = WorkFlowAPI.UpdateWf(WorkflowUpdateWrapper);
            $q.all([data.$promise]).then(function (response) {
                $(".loader").hide();
                $modalInstance.dismiss("success");
        });
        }
        $scope.Reject = function () {
            var WFAprroveReject = {
                "prop_wf_reviewOutcome": "Reject",
                "prop_bpm_comment": $scope.wf.comment,
                "prop_transitions": "Next",
                "prop_bpm_status": $scope.wf.Status.value
            }
            var model = {
                activitiid: items.pid,
                WFAprroveReject: WFAprroveReject
            }
            $(".loader").show();
            var data = WorkFlowAPI.ApproveReject(model)
            $q.all([data.$promise]).then(function (response) {
             $(".loader").hide();
                $modalInstance.dismiss("success");
            });
        }
        $scope.Approve = function () {
            var WFAprroveReject = {
                "prop_wf_reviewOutcome": "Approve",
                "prop_bpm_comment": $scope.wf.comment,
                "prop_transitions": "Next",
                "prop_bpm_status": $scope.wf.Status.value
            }

            var model = {
                activitiid: items.pid,
                WFAprroveReject: WFAprroveReject
            }
                  $(".loader").show();
            var data = WorkFlowAPI.ApproveReject(model)
            $q.all([data.$promise]).then(function (response) {
                  $(".loader").hide();
                $modalInstance.dismiss("success");
            });
        }
        $scope.EndTask = function () {
            var wfDone = {
                "assoc_packageItems_added": "",
                "assoc_packageItems_removed": "",
                "prop_bpm_comment": $scope.wf.comment,
                "prop_transitions": "Next",
                "prop_bpm_status": $scope.wf.Status.value
            }
            var model = {
                activitiid: items.pid,
                wfDone: wfDone
            }
                 $(".loader").show();
            var data = WorkFlowAPI.DoneTask(model);
            $q.all([data.$promise]).then(function (response) {
                $(".loader").hide();
                $modalInstance.dismiss("success");
            });
        }
        $scope.ReassignTask = function () {
            var IsResolved = false;
            if (Profile.get('Profile').User.userName == items.creatorUserName) {
                IsResolved = true;
            }
            ReassignModel.taskId = items.taskId;
            ReassignModel.username = items.creatorUserName;
            ReassignModel.IsResolve = IsResolved;
            ReassignModel.comment = $scope.wf.comment;
                 $(".loader").show();
            var data = WorkFlowAPI.Reassign(ReassignModel);
            $q.all([data.$promise]).then(function (response) {
                $modalInstance.dismiss("success");
                    $(".loader").hide();
            });
        }
        Bind();
        $scope.cGridDataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success(grddata);
                }
            },
            sort:  [
        {
            field: "cm_created", dir: "desc"
        },
        {field: "id", dir: "desc"}
        ],
            
            pageSize: 1000,
            schema: {
                model: {
                    action: "",
                        fields: {
                         "id":{ 
                            type:'int'
                         },
                        "title": {
                            type: "string", editable: false
                        },
                        "cm_owner": {
                            type: "string", editable: false
                        },
                        status: {
                            type: "string",
                            editable: false
                        },
                        "cm_created": {
                            type: "date", editable: false
                        },
                        "bpm_comment": {
                            type: "string", editable: false
                        }
                    }
                }
            },
        });
        $scope.cGridOption = {
            dataSource: $scope.cGridDataSource,
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
            height: 120,
            selectable: "row",
            filterable: true,
            footer: false,
            columns: [
                     { field: "title", "title": "Type" },

                { field: "cm_owner", "title": "Assigned to"},
            {
                field: "status", title: "Status"
            },
            {
                field: "bpm_comment", title: "Comment"
            },
            {
                field: "cm_created", title: "Created", template: "#= kendo.toString(kendo.parseDate(cm_created), 'dd MMM yyyy hh:mm:ss') #"
            }
            ]
        }
    }]);
})();