(function () {
    'use strict';
    app.controller('FolderController', ['$scope','$rootScope', 'FolderAPI', '$q',
function ($scope, $rootScope, FolderAPI, $q) {
    var vm = this;
    vm.treeData = null;
    vm.selectedItem;
    function Bind() {
        var data = FolderAPI.GetRootFolders();
        $q.all([data.$promise]).then(function (response) {
            vm.treeData = new kendo.data.HierarchicalDataSource({
                data: response[0]
            });
        });
    }
    vm.Select = function () {
        alert(vm.selectedItem)
    }
    Bind();
}])
})();
