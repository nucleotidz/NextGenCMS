(function () {
    'use strict';
    app.controller('FolderController', ['$scope', '$rootScope', 'FolderAPI', 'FileAPI', '$q', '$modal', 'Global',
function ($scope, $rootScope, FolderAPI, FileAPI, $q, $modal, Global) {
    var vm = this;
    var node;
    var path
    vm.treeData = null;
    var nodeRefs = [];
    var Files = [];
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
    };
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
        var fileDate = FileAPI.GetFiles(SubFolderModel);
        $q.all([apiData.$promise, fileDate.$promise]).then(function (response) {
            if (response[1].items !== undefined && response[1].items.length > 0) {
                Files = _.where(response[1].items, { isFolder: false });
                vm.FileGridDataSource.read();
            }
            else {
                Files = [];
                vm.FileGridDataSource.read();
            }
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
                nodeRefs.push(response[0].noderef);

            });
        });
    }

    vm.FileGridDataSource = new kendo.data.DataSource({
        type: "json",
        transport: {
            read: function (o) {
                o.success(Files);
            }
        },
        pageSize: 1000,
        schema: {
            model: {
                action: "",
                fields: {
                    displayName: {
                        type: "string", editable: false
                    },
                    version: {
                        type: "string", editable: false
                    },
                    lockedByUser: {
                        type: "string", editable: false
                    },
                    createdOn: {
                        type: "string", editable: false
                    },
                    path: {
                        type: "string", editable: false
                    },
                    nodeRef: {
                        type: "string", editable: false
                    },
                    webdavUrl: {
                        type: "string", editable: false
                    }
                }
            }
        },
    });
    vm.FileGridOptions = {
        dataSource: vm.FileGridDataSource,
        dataBound: function () {
        },
        sortable: {
            mode: "multiple",
            allowUnsort: true
        },
        reorderable: true,
        resizable: true,
        navigatable: true,
        scrollable: true,
        selectable: "row",
        filterable: true,
        pageable: {
            numeric: false,
            previousNext: false,
            messages: {
                empty: "No Records exist",
                display: "No of records is: {2}"
            }
        },
        columns: [
        {
            field: "displayName", title: "Name", filterable: true
        },
        {
            field: "version", title: "Version"
        },
        {
            field: "lockedByUser", title: "Locked By"
        },
        {
            field: "createdOn", title: "Created On", template: "#= kendo.toString(kendo.parseDate(createdOn), 'dd MMM yyyy') #"
        },
        {
            field: "modifiedOn", title: "Modified On", template: "#= kendo.toString(kendo.parseDate(modifiedOn), 'dd MMM yyyy') #"
        },
        {
            field: "path", title: "path", hidden: true
        },
        {
            field: "nodeRef", title: "nodeRef", hidden: true
        },
        {
            field: "webdavUrl", hidden: true
        }]
    }
    vm.Download = function () {
        var entityGrid = $("#userGrid").data("kendoGrid")
        var selectedItem = entityGrid.dataItem(entityGrid.select());
        window.open(Global.Alfresco + selectedItem.webdavUrl);
    }
    Bind();
}])
})();
