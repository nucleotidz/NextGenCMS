(function () {
    'use strict';
    app.controller('ToolPaneController', ['$scope', 'UserProfile',
    function ($scope, UserProfile) {
        var vm = this;
        vm.isAdminUser = UserProfile.isAdminUser();
    }]);
})();