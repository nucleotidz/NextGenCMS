(function () {
    'use strict';
    app.controller('ViewEditWfController', ['$scope','$rootScope', '$modalInstance', 'items', 'Global','$timeout',
    function ($scope,$rootScope, $modalInstance, items, Global,$timeout) {
        $scope.wf = {
            TaskEnable: false,
            ActionEnable: true,
            Owner: items.fullName,
            Priority: items.priority,
            DueDate: "",
            Status: "",
            StatusDataSource: [],
            disable: true
        }
        $scope.wf.StatusDataSource.push({ "text": "Not Yet Started","value": "Not Yet Started"  })
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
        },150);      
       
        $scope.closePopup = function () {
            $modalInstance.dismiss("close");
        }
    }]);
})();