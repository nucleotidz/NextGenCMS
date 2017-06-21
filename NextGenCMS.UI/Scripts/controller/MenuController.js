(function () {
    'use strict';
    app.controller('MenuController', ['$scope', 'UserProfile',
    function ($scope, UserProfile) {
        var vm = this;
        vm.isAdminUser = UserProfile.isAdminUser();
    }]);
})();
