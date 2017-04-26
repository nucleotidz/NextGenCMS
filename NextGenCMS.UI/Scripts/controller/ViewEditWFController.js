(function () {
    'use strict';
    app.controller('ViewEditWfController', ['$scope', '$rootScope', '$modalInstance', 'items', 'Global', '$timeout', 'WorkFlowAPI', '$q', '$http',
    function ($scope, $rootScope, $modalInstance, items, Global, $timeout, WorkFlowAPI, $q, $http) {
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
            comment: items.comment
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
            $q.all([data.$promise]).then(function (response) {
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
                "prop_bpm_comment": "next",
                "prop_transitions": "Next"
            }
            //{
            //    "prop_wf_reviewOutcome":"Approve",
            //    "prop_bpm_comment":"next",
            //    "prop_transitions":"Next"
            //}
            $http({
                method: 'POST',
                url: 'http://127.0.0.1:8080/alfresco/service/api/task/' + items.pid + '/formprocessor?alf_ticket=TICKET_2d4a032994510f068d25a668e33522ce5d8aca38',
                data:WFAprroveReject
            }).then(function successCallback(response) {
                alert('Success');
            }, function errorCallback(response) {
                alert('Error');
            });
            var model = {
                activitiid: items.pid,
                WFAprroveReject: WFAprroveReject
            }

            var data = WorkFlowAPI.ApproveReject(model)
            $q.all([data.$promise]).then(function (response) {
                $modalInstance.dismiss("success");
            });
        }
        Bind();
    }]);
})();