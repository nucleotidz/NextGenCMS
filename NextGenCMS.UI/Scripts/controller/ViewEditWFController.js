(function () {
    'use strict';
    app.controller('ViewEditWfController', ['$scope', '$rootScope', '$modalInstance', 'items', 'Global', '$timeout', 'WorkFlowAPI', '$q',
    function ($scope, $rootScope, $modalInstance, items, Global, $timeout, WorkFlowAPI, $q) {
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
            desc: items.description
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

            $scope.wf.Status = items.status;
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
        Bind();
    }]);
})();