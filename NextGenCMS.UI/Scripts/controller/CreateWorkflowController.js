(function () {
    'use strict';
    app.controller('CreateWorkflowController', ['$scope', '$modalInstance', 'WorkFlowAPI', 'AdministrationApi', '$q', 'Profile', 'items',
    function ($scope, $modalInstance, WorkFlowAPI, AdministrationApi, $q, Profile, items) {
        $scope.userName = Profile.get('Profile').User.userName;
        $scope.UserData = new kendo.data.DataSource();
        $scope.DocID = items.docId;
        $scope.FileName = items.FileName;
        $scope.isgroupreview = false;       
        $scope.WdTypeDataSrc = [{
            "text": "Assign a review task to a single reviewer", "value": "activitiReview"
        },
        {
            "text": "Pooled Review And Approve Activiti Process", "value": "activitiReviewPooled"
        }]
        $scope.groupDataSource = new kendo.data.DataSource();
        function init() {
            var data = AdministrationApi.getGroups();
            var userdata = AdministrationApi.getUsers({ "username": $scope.userName });
            $q.all([data.$promise, userdata.$promise]).then(function (response) {
                $scope.groupDataSource = response[0].data;
                $scope.UserData = response[1].people;
                 var dropdownlist =  $('#dpworkfType').data('kendoDropDownList');
                dropdownlist.value('activitiReview')
                dropdownlist.trigger("change");
            });
        };      
        $scope.WFModelWrapper = {
            workModel: '',
            docId : ''
        }      
        $scope.wf = {
            processDefinitionKey: "activitiReview",
            variables: {
                bpm_assignee: "",
                bpm_assigneeGrp: "",
                bpm_sendEMailNotifications: false,
                bpm_workflowPriority: "2",
                bpm_workflowDueDate: "",
                bpm_workflowDescription: "",
                bpm_groupAssignee: "",
                bpm_comment: ""
            }
        }      
        $scope.OnWofklowTypeChange = function (e) {
            var item = e.sender.dataItem();
            if (item.value === 'activitiReviewPooled') {
                $scope.isgroupreview = true;
            }
        }

        $scope.priorityDataSrc = [{
            "text": "High", "value": "1"
        },
        {
            "text": "Medium", "value": "2"
        },
        {
            "text": "Low", "value": "3"
        }]
        $scope.workfdpOptions = {
            dataSource: $scope.WdTypeDataSrc,
            dataTextField: "text",
            dataValueField: "value"           
        }

        $scope.createWorkflow = function () {
            $scope.wf.processDefinitionKey = $scope.wf.processDefinitionKey.value;
            if($scope.isgroupreview){
                $scope.wf.variables.bpm_groupAssignee = $scope.wf.variables.bpm_assigneeGrp.fullName;
            }
            else {
                $scope.wf.variables.bpm_assignee = $scope.wf.variables.bpm_assignee.userName;
            }
            $scope.WFModelWrapper.workModel = $scope.wf;
            $scope.WFModelWrapper.docId = $scope.DocID;
            var submitdata = WorkFlowAPI.CreateWorkflow($scope.WFModelWrapper);
            $q.all(submitdata.$promise).then(function (response){
                $modalInstance.dismiss("success");
            });
        }
        $scope.closeWorkflowPopup = function () {
            $modalInstance.dismiss("close");
        };
        init();
    }]);
})();