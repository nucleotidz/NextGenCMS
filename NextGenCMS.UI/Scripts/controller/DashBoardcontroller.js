(function () {
    'use strict';
    app.controller('DashboardController', ['$scope', 'WorkFlowAPI', '$q', 'DataSharingService', 'FolderAPI', 'UserProfile',
    function ($scope, WorkFlowAPI, $q, DataSharingService, FolderAPI, UserProfile) {
        var vm = this;
        var userName = UserProfile.getUserName();
        vm.Name = UserProfile.getUserDisplayName();
        vm.isAdminUser = UserProfile.isAdminUser();
        vm.data = DataSharingService.data;
        var tasks = WorkFlowAPI.GetWorkFlow();
        var checkoutCount = FolderAPI.CheckOutCountByUser({ userName: userName });
        $(".loader").show();
        $q.all([tasks.$promise, checkoutCount.$promise]).then(function (response) {
            if (response[0].length > 0) {
                DataSharingService.data.taskCount = response[0].length;
            }
            DataSharingService.data.checkOutCount = response[1].totalItems;
            $(".loader").hide();
        });
    }]);
})();