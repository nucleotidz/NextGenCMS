(function () {
    'use strict';
    app.controller('DashboardController', ['$scope', 'WorkFlowAPI', '$q', 'DataSharingService', 'FolderAPI', 'Profile',
    function ($scope, WorkFlowAPI, $q, DataSharingService, FolderAPI, Profile) {
        var vm = this;
        var userName = Profile.get('Profile').User.userName;
        vm.data = DataSharingService.data;
        var tasks = WorkFlowAPI.GetWorkFlow();
        var checkoutCount = FolderAPI.CheckOutCountByUser({ userName: userName });
        $q.all([tasks.$promise, checkoutCount.$promise]).then(function (response) {
            if (response[0].length > 0) {
                DataSharingService.data.taskCount = response[0].length;
            }
            DataSharingService.data.checkOutCount = response[1].totalItems;
        });
    }]);
})();