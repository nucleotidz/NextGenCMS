(function () {
    'use strict';
    app.controller('HeaderController', ['$scope', 'Cache',
    function ($scope, Cache) {
        var vm = this;
        var displayName = Cache.get('displayName');
        vm.Name = displayName;
    }]);
})();
