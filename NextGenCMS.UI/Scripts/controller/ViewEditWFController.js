(function () {
    'use strict';
    app.controller('ViewEditWfController', ['$scope', '$rootScope', '$modalInstance', 'items', 'Global', '$timeout', 'WorkFlowAPI', '$q', '$http','Profile',
    function ($scope, $rootScope, $modalInstance, items, Global, $timeout, WorkFlowAPI, $q, $http, Profile) {
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
            comment: items.comment,
            ActionOccured: true,          
        }
        if ((items.outcome == "Approved" || items.outcome == "Rejected") && items.ownerUsername == Profile.get('Profile').User.userName) {
            $scope.wf.ActionOccured = false;
            $scope.wf.TaskEnable = true;
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
                    $scope.wf.fileName = response[0].list.entries[0].entry.name
                    $scope.wf.fileId = response[0].list.entries[0].entry.id
                }
            });
        }
        $scope.DownloadFile = function (id) {
            var formId = "formFile";
            angular.element("body").append("<form  method='POST' id='" + formId + "' action='" + Global.apiuri + "File/Download/By/Id" + "' target='_tab' >");
            $("#" + formId + "").append("<input type='hidden' value='" + id + "'  name='id' >");
            angular.element("#" + formId + "").submit();
            angular.element("#" + formId + "").remove();
        }
        $scope.Save = function () {
            var WFUpdate = [];
            WFUpdate.push({ "name": "bpm_comment", "value": $scope.wf.comment, "type": "d:text", "scope": "local" })
            WFUpdate.push({ "name": "bpm_status", "value": $scope.wf.Status.value, "type": "d:text", "scope": "local" })
            var WFUpdateModel = {
                wfId: items.taskId,
                WFUpdate: WFUpdate
            }
            var data = WorkFlowAPI.UpdateWf(WFUpdateModel)
            $q.all([data.$promise]).then(function (response) {
                $modalInstance.dismiss("success");
            });
        }
        $scope.Reject = function () {
            var WFAprroveReject = {
                "prop_wf_reviewOutcome": "Reject",
                "prop_bpm_comment": $scope.wf.comment,
                "prop_transitions": "Next"
            }
            var model = {
                activitiid: items.pid,
                WFAprroveReject: WFAprroveReject
            }
            var data = WorkFlowAPI.ApproveReject(model)
            $q.all([data.$promise]).then(function (response) {
                $modalInstance.dismiss("success");
            });
        }
        $scope.Approve = function () {
            var WFAprroveReject = {
                "prop_wf_reviewOutcome": "Approve",
                "prop_bpm_comment": $scope.wf.comment,
                "prop_transitions": "Next"
            }
            
            var model = {
                activitiid: items.pid,
                WFAprroveReject: WFAprroveReject
            }

            var data = WorkFlowAPI.ApproveReject(model)
            $q.all([data.$promise]).then(function (response) {
                $modalInstance.dismiss("success");
            });
        }
        $scope.EndTask = function () {
            var wfDone=  {
                "prop_bpm_status":"Not Yet Started",
                "assoc_packageItems_added":"",
                "assoc_packageItems_removed":"",
                "prop_bpm_comment": $scope.wf.comment,
                "prop_transitions":"Next"
            }
            var model = {
                activitiid: items.pid,
                wfDone: wfDone
            }
            var data = WorkFlowAPI.DoneTask(model);
            $q.all([data.$promise]).then(function (response) {
                $modalInstance.dismiss("success");
            });
        }
        Bind();
    }]);
})();