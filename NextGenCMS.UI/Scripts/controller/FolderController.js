(function () {
    'use strict';
    app.controller('FolderController', ['$scope', '$rootScope', 'FolderAPI', 'FileAPI', '$q', '$modal', 'Global', 'Cache', '$state',
function ($scope, $rootScope, FolderAPI, FileAPI, $q, $modal, Global, Cache, $state) {
    var vm = this;
    var node;
    var path
    var deleteData = {
        path: "",
        entity: "",
        data: "",
    }
    var officeExtentionList = [
        'doc',
        'docx',
        'docm',
        'dot',
        'dotx',
        'dotm',
        'xls',
        'xlsx',
        'xlsb',
        'xlsm',
        'xlt',
        'xltx',
        'xltm',
        'xlsm',
        'ppt',
        'pptx',
        'pot',
        'potx',
        'potm',
        'pptm',
        'potm',
        'pps',
        'ppsx',
        'ppam',
        'ppsm',
        'sldx',
        'sldm'];
    var msProtocolNames =
         {
             'doc': 'ms-word',
             'docx': 'ms-word',
             'docm': 'ms-word',
             'dot': 'ms-word',
             'dotx': 'ms-word',
             'dotm': 'ms-word',
             'xls': 'ms-excel',
             'xlsx': 'ms-excel',
             'xlsb': 'ms-excel',
             'xlsm': 'ms-excel',
             'xlt': 'ms-excel',
             'xltx': 'ms-excel',
             'xltm': 'ms-excel',
             'ppt': 'ms-powerpoint',
             'pptx': 'ms-powerpoint',
             'pot': 'ms-powerpoint',
             'potx': 'ms-powerpoint',
             'potm': 'ms-powerpoint',
             'pptm': 'ms-powerpoint',
             'pps': 'ms-powerpoint',
             'ppsx': 'ms-powerpoint',
             'ppam': 'ms-powerpoint',
             'ppsm': 'ms-powerpoint',
             'sldx': 'ms-powerpoint',
             'sldm': 'ms-powerpoint',
         };
    $scope.rawFile = null;
    $scope.rawFileName = '';
   
    vm.treeData = null;
    var nodeRefs = [];
    var Files = [];
    vm.TreeSelect = true;
    vm.orientation = "vertical";
    function Bind() {
        var data = FolderAPI.GetRootFolders();
        $(".loader").show();
        $q.all([data.$promise]).then(function (response) {
            for (var i = 0; i < response[0].length; i++) {
                nodeRefs.push(response[0][i].noderef)
            }
            vm.treeData = new kendo.data.HierarchicalDataSource({
                data: response[0]

            });
            $(".loader").hide();
        });
    };
    vm.selectedItem = function (data) {
        if (data === undefined) {
            return;
        }
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
        vm.TreeSelect = false;
        $(".loader").show();
        $q.all([apiData.$promise, fileDate.$promise]).then(function (response) {
            $(".loader").hide();
            if (response[1].items !== undefined && response[1].items.length > 0) {
                Files = _.where(response[1].items, {
                    isFolder: false
                });
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
        $(".loader").show();
        $q.all([fileDate.$promise]).then(function (response) {
            if (response[0].items !== undefined && response[0].items.length > 0) {
                Files = _.where(response[0].items, {
                    isFolder: false
                });
                vm.FileGridDataSource.read();
            }
            else {
                Files = [];
                vm.FileGridDataSource.read();
            }
            $(".loader").hide();
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
            $(".loader").show();
            $q.all([apiData.$promise]).then(function (response) {
                vm.tree.append({
                    "name": response[0].name, "title": response[0].title, "description": response[0].description, "noderef": response[0].noderef, hasChildren: response[0].hasChildren
                });
                nodeRefs.push(response[0].noderef)
                $(".loader").hide();
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
            $(".loader").show();
            $q.all([apiData.$promise]).then(function (response) {
                vm.tree.append({ "name": response[0].name, "title": response[0].title, "description": response[0].description, "noderef": response[0].noderef, hasChildren: response[0].hasChildren }, vm.tree.select());
                nodeRefs.push(response[0].noderef);
                $(".loader").hide();
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
                OfficeURL:  function calculateOfficeURL() {
                    var pos = officeExtentionList.indexOf(this.displayName.split('.').pop());
                    if (pos > -1) {
                        var proto = msProtocolNames[this.displayName.split('.').pop()]
                        var path = this.webdavUrl.replace('/webdav', '');
                        return proto + ':ofe%7Cu%7C' + Global.Alfresco + 'aos' + path;
                    }
                },
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
        height: 300,
        selectable: "row",
            filterable: true,
            footer: false,      
        columns: [
        {
            field: "displayName", title: "Name", filterable: true, template: function (dataitem) {
                if (dataitem.lockedBy !== '') {
                    return dataitem.displayName + "&nbsp<span class='glyphicon glyphicon-tags'></span>";
                               }
                                   else {
                                   return dataitem.displayName;
                                   }
                },
            },
        {
            field: "version", title: "Version", template: function (dataitem) {
                if (dataitem.lockedBy !== '') {
                                   return '';
                               }
                                   else {
                                   return dataitem.version;
                                   }
                }
        },
        {
            field: "status", title: "Checked Out", template: "#if (lockedBy !== '') {# yes #} else {# no #}  #"
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
            }     
        ]
     }


     function addLock(displayName) {
            return "<span class='glyphicon glyphicon-lock' > </span> " + displayName;
     }
    function Download() {
        var entityGrid = $("#userGrid").data("kendoGrid")
        var selectedItem = entityGrid.dataItem(entityGrid.select());              
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
        if (evt.item.textContent.trim() === "Delete") {
            DeleteFile();
        }
        if (evt.item.textContent.trim() === "Cancel Check-Out") {
            CancelCheckOut();
        }
        if (evt.item.textContent.trim() === "Start Workflow") {
        OpenCreateWFPopup();
        }
        if (evt.item.textContent.trim() === "Properties") {
            OpenMeta();
        }
            if (evt.item.textContent.trim() === "Edit in office") {
                EditFile();
            }
    }
        function EditFile() {
            var entityGrid = $("#userGrid").data("kendoGrid")
            var selectedItem = entityGrid.dataItem(entityGrid.select());
            var anchorID = "anchor";
            angular.element("body").append("<a  id='" + anchorID + "' href='" + selectedItem.OfficeURL() + "' target='_tab' >");
            angular.element("#" + anchorID + "")[0].click();
            angular.element("#" + anchorID + "").remove();
            refreshFileGrid();

        }
    function OpenMeta() {
        var entityGrid = $("#userGrid").data("kendoGrid")
        var selectedItem = entityGrid.dataItem(entityGrid.select());
        var fileName = selectedItem.displayName;
        var fileMeta = _.where(Files, {
            displayName: fileName
        });
        var modalInstance = $modal.open({
            backdrop: 'static',
            keyboard: false,
            templateUrl: 'Folder/Metadata',
            controller: 'MetaDataController',
            resolve: {
                items: function () {
                    return fileMeta;
                }
            }
        });
       // $state.go("Home.Metadata", { fileMeta: fileMeta });
    }

    function OpenCreateWFPopup() {
      var entityGrid = $("#userGrid").data("kendoGrid")
      var selectedItem = entityGrid.dataItem(entityGrid.select());
      var fileName = selectedItem.displayName;
     var splitList = selectedItem.nodeRef.split("/");
            var objId = splitList[splitList.length - 1];
      var modalInstance = $modal.open({
                backdrop: 'static',
        keyboard: false,
        templateUrl: './Workflow/CreateWorkflow',
        controller: 'CreateWorkflowController',
        resolve: {
                    items: function () {
                        return {
                            "docId": objId,
                       "FileName": fileName
              }
            }
              }
                  });
                        modalInstance.result.then(function () { 
        }, function (popupData) {
            if (popupData === "success") {
                refreshFileGrid();
                  }
                  });
                  }

    function DeleteFile() {
        var entityGrid = $("#userGrid").data("kendoGrid")
        var selectedItem = entityGrid.dataItem(entityGrid.select());
        deleteData.path = path;
        deleteData.entity = "file"
        deleteData.data = selectedItem.displayName
        var modalInstance = $modal.open({
            backdrop: 'static',
            keyboard: false,
            templateUrl: './Folder/Delete',
            controller: 'DeleteController',
            resolve: {
                items: function () {
                    return deleteData;
                }
            }
        });
        modalInstance.result.then(function () {
        }, function (popupData) {
            if (popupData === "success") {
                refreshFileGrid();
            }
        });
    };
    vm.DeleteFolder = function () { 
        deleteData.path = path;
        deleteData.entity = "folder"
        deleteData.data = node
        var modalInstance = $modal.open({
            backdrop: 'static',
            keyboard: false,
            templateUrl: './Folder/Delete',
            controller: 'DeleteController',
            resolve: {
                items: function () {
                    return deleteData;
                }
            }
        });
        modalInstance.result.then(function () {
        }, function (popupData) {
            if (popupData === "success") {
                var array = node.parent();
                var index = array.indexOf(node);
                array.splice(index, 1);
                remove(node.noderef)
                node = null;
                path = '';
                vm.TreeSelect = true;
                Files = [];
                vm.FileGridDataSource.read();
                if (vm.treeData._data.length < 1) {
                    nodeRefs = [];
                }
            }
        });
    };
    function remove(item) {
        for (var i = nodeRefs.length; i--;) {
            if (nodeRefs[i] === item) {
                nodeRefs.splice(i, 1);
                break;
            }
        }
    }
    function Checkout() {
        var entityGrid = $("#userGrid").data("kendoGrid")
        var selectedItem = entityGrid.dataItem(entityGrid.select());
        if (selectedItem.lockedBy !== '') {
            showAlert("File already checked out by " + selectedItem.lockedBy, "danger");
            return;
            }      
        var name = selectedItem.displayName;
        var CheckoutParamsModel = {
            path: '',
            site: "ahmar",
            container: "documentLibrary"
        }
        CheckoutParamsModel.path = path + "/" + name;
        var apiData = FolderAPI.CheckOutFile(CheckoutParamsModel);
        $(".loader").show();
        $q.all([apiData.$promise]).then(function (response) {
            refreshFileGrid();
            $(".loader").hide();
        });
    }
    function showAlert(msg, type) {
        alert(msg);
    }

    function Checkin() {
        var entityGrid = $("#userGrid").data("kendoGrid")
        var selectedItem = entityGrid.dataItem(entityGrid.select());
        if (selectedItem.lockedBy === '') {
          showAlert("File not checked out", "danger");
          return;
       }
        var splitList = selectedItem.nodeRef.split("/");
        var objId = splitList[splitList.length - 1];
        var apiData = FolderAPI.CheckInFile({
            "objectId": objId
        })
        $(".loader").show();
        $q.all([apiData.$promise]).then(function (response) {
            refreshFileGrid();
            $(".loader").hide();
        });
    }

   function CancelCheckOut() {
        var entityGrid = $("#userGrid").data("kendoGrid")
        var selectedItem = entityGrid.dataItem(entityGrid.select());
        if (selectedItem.lockedBy === '') {
          showAlert("File not checked out", "danger");
              return;
              }
            var splitList = selectedItem.nodeRef.split("/");
            var objId = splitList[splitList.length - 1];
        var apiData = FolderAPI.CancelCheckOut({
            "objectId": objId
        })
        $(".loader").show();
        $q.all([apiData.$promise]).then(function (response) {
            refreshFileGrid();
            $(".loader").hide();
            });
          };
    vm.open = function (evt) {
        var entityGrid = $("#userGrid").data("kendoGrid")
        var selectedItem = entityGrid.dataItem(entityGrid.select());
        if (selectedItem == null) {
            evt.preventDefault();
            return;
        }

    }
    $scope.upload = function () {
        if (path == undefined) {
            return;
        }
        var formdata = new FormData();
        formdata.append("tenant", tenant);
        formdata.append("path", path);
        formdata.append($scope.rawFileName, $scope.rawFile);
        var xhr = new XMLHttpRequest();
        $(".loader").show();
        xhr.open("POST", Global.apiuri + "File/Upload", false);
        xhr.send(formdata);
        $(".loader").hide();
        refreshFileGrid();
    }
    Bind();
}])
})();
