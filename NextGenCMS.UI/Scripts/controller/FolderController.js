(function () {
    'use strict';
    app.controller('FolderController', ['$scope', '$rootScope', 'FolderAPI', '$q',
function ($scope, $rootScope, FolderAPI, $q) {
    var vm = this;
    vm.treeData = null;
    var nodeRefs = [];
    function Bind() {
        var data = FolderAPI.GetRootFolders();
        $q.all([data.$promise]).then(function (response) {
            for (var i = 0; i < response[0].length; i++) {
                nodeRefs.push(response[0][i].noderef)
            }
            vm.treeData = new kendo.data.HierarchicalDataSource({
                data: response[0]

            });
        });
    }
    vm.selectedItem = function (data) {
        var hasChildren = data.hasChildren;
        var path = data.name;
        while (data.parentNode() !== undefined) {
            data = data.parentNode();
            path = data.name + "/" + path
        }
       // if (hasChildren) {
            var SubFolderModel = {
                path: path
            }
            var apiData = FolderAPI.GetSubFolderFolders(SubFolderModel)
            $q.all([apiData.$promise]).then(function (response) {
                if (response[0].length > 0) {
                    for (var i = 0; i < response[0].length; i++) {
                        if (nodeRefs.indexOf(response[0][i].noderef) > -1) {
                            return;
                        }
                        nodeRefs.push(response[0][i].noderef)
                    }
                    vm.tree.append(response[0], vm.tree.select());
                }
            });
        //}
    }
    Bind();
}])
})();
