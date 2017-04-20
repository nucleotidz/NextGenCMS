(function () {
    'use strict';
    app.controller('FolderController', ['$scope', '$rootScope', 'FolderAPI', '$q', '$modal',
function ($scope, $rootScope, FolderAPI, $q, $modal) {
    var vm = this;
    var node;
    var path
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
        node = data;
        path = data.name;
        while (data.parentNode() !== undefined) {
            data = data.parentNode();
            path = data.name + "/" + path
        }
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
    }
    vm.AddFolder = function () {
        var modalInstance = $modal.open({
            backdrop: 'static',
            keyboard: false,
            templateUrl: './Folder/AddFolderPopup',
            controller: 'AddFolderPopupController'
        });
        modalInstance.result.then(function (item) {
            var FolderModel = item;
            var apiData = FolderAPI.CreateFolder(FolderModel)
            $q.all([apiData.$promise]).then(function (response) {
                vm.tree.append({ "name": response[0].name, "title": response[0].title, "description": response[0].description, "noderef": response[0].noderef, hasChildren: response[0].hasChildren });
                nodeRefs.push(response[0].noderef)
            });

        });
    }
    vm.AddSubFolder = function () {
        if (vm.tree.select().length < 1) {
            return;
        }
        var modalInstance = $modal.open({
            backdrop: 'static',
            keyboard: false,
            templateUrl: './Folder/AddFolderPopup',
            controller: 'AddFolderPopupController'
        });
        modalInstance.result.then(function (item) {
            var modaldata = item;
            var FolderModel = {
                path: path,
                folder: modaldata
            }
            var apiData = FolderAPI.CreateSubFolder(FolderModel)
            $q.all([apiData.$promise]).then(function (response) {
                vm.tree.append({ "name": response[0].name, "title": response[0].title, "description": response[0].description, "noderef": response[0].noderef, hasChildren: response[0].hasChildren }, vm.tree.select());
                nodeRefs.push(response[0].noderef)
            });

        });
    }
   
    Bind();
}])
})();
