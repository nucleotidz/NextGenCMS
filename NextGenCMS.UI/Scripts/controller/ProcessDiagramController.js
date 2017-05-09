(function () {
    'use strict';
    app.controller('ProcessDiagramController', ['$scope', '$modalInstance', 'Cache', 'items', '$timeout',
    function ($scope, $modalInstance, Cache, items, $timeout) {
        var token = Cache.get('token');
        $scope.init = function () {
            $(".loader").show();
            $scope.workflowInstanceId = items;
            var formId = "formFile";
            var url = 'http://cscindag970280:8080/alfresco/s/api/workflow-instances/' + $scope.workflowInstanceId + '/diagram?alf_ticket=' + token;
            angular.element("body").append("<form  method='GET' id='" + formId + "' action='" + url + "' target='myIframe' >");
            angular.element("#" + formId + "").submit();
            angular.element("#" + formId + "").remove();

            $timeout(function () {
                $(".loader").hide();                
            }, 500);
        }

        $scope.closePopup = function () {
            $modalInstance.dismiss("close");
        };
    }]);
})();