(function () {
    'use strict';
    app.controller('FolderController', ['$scope', '$rootScope', 'FolderAPI', 'FileAPI', '$q', '$modal', 'Global', 'Cache',
function ($scope, $rootScope, FolderAPI, FileAPI, $q, $modal, Global, Cache) {
    var vm = this;
    var node;
    var path
    vm.treeData = null;
    var nodeRefs = [];
    var Files = [];
    vm.orientation = "vertical";
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
    function refreshFileGrid() {
        var SubFolderModel = {
            path: path
        }
        var fileDate = FileAPI.GetFiles(SubFolderModel);
        $q.all([fileDate.$promise]).then(function (response) {
            if (response[0].items !== undefined && response[0].items.length > 0) {
                Files = _.where(response[0].items, { isFolder: false });
                vm.FileGridDataSource.read();
            }
            else {
                Files = [];
                vm.FileGridDataSource.read();
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
    vm.AddFile = function () {
        var modalInstance = $modal.open({
            backdrop: 'static',
            keyboard: false,
            templateUrl: './Folder/Upload',
            controller: 'UploadController',
            resolve: {
                items: function () {
                    return path;
                }
            }
        });
        modalInstance.result.then(function () {
                            }, function (popupData) {
                                refreshFileGrid();
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
                    //lockedByUser: {
                    //    type: "string", editable: false
                    //},
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
                    },
                    contentUrl: {
                        type: "string", editable: false
                    },
                    status: {
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
            field: "status", title: "Checked Out", template: "#if (status == 'editing') {# yes #} else {# no #}  #"
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
        },
            {
                field: "contentUrl", hidden: true
            }]
    }
    function Download() {
        var entityGrid = $("#userGrid").data("kendoGrid")
        var selectedItem = entityGrid.dataItem(entityGrid.select());
        //window.open(Global.Alfresco + selectedItem.webdavUrl + "?alf_ticket=" + Cache.get("token"));           
        var formId = "formFile";
        angular.element("body").append("<form  method='POST' id='" + formId + "' action='" + Global.apiuri + "File/Download" + "' target='_tab' >");
        $("#" + formId + "").append("<input type='hidden' value='" + selectedItem.contentUrl + "'  name='path' >");
        $("#" + formId + "").append("<input type='hidden' value='" + selectedItem.displayName + "'  name='name' >");
        $("#" + formId + "").append("<input type='hidden' value='" + Cache.get("token") + "'  name='ticket' >");
        angular.element("#" + formId + "").submit();
        angular.element("#" + formId + "").remove();

    }
    vm.onSelect = function (evt) {
        if (evt.item.textContent.trim() === "Download") {
            Download();
        }
        if (evt.item.textContent.trim() === "Check-Out") {
            Checkout();
        }
        if (evt.item.textContent.trim() === "Check-In") {
            Checkin();
        }
    }
    function Checkout() {
        var entityGrid = $("#userGrid").data("kendoGrid")
        var selectedItem = entityGrid.dataItem(entityGrid.select());
        var name = selectedItem.displayName;
        var CheckoutParamsModel = {
            path: '',
            site: "ahmar",
            container: "documentLibrary"
        }
        CheckoutParamsModel.path = path + "/" + name;
        var apiData = FolderAPI.CheckOutFile(CheckoutParamsModel);
        $q.all([apiData.$promise]).then(function (response) {
            refreshFileGrid();
        });
    }

    function Checkin() {
        var entityGrid = $("#userGrid").data("kendoGrid")
        var selectedItem = entityGrid.dataItem(entityGrid.select());
        var splitList = selectedItem.nodeRef.split("/");
        var objId = splitList[splitList.length - 1];
        var apiData = FolderAPI.CheckInFile({ "objectId": objId })
        $q.all([apiData.$promise]).then(function (response) {
            refreshFileGrid();
        });
    }
    vm.open = function (evt) {
        var entityGrid = $("#userGrid").data("kendoGrid")
        var selectedItem = entityGrid.dataItem(entityGrid.select());
        if (selectedItem == null) {
            evt.preventDefault();
            return;
        }
    }
    Bind();
}])
})();
