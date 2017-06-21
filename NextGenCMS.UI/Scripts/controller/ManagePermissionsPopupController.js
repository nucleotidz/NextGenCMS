(function () {
    'use strict';
    app.controller('ManagePermissionsPopupController', ['$scope', '$modalInstance', 'AdministrationApi', '$q', 'items',
    function ($scope, $modalInstance, AdministrationApi, $q, items) {
        $scope.permissionDataSource = new kendo.data.DataSource();
        $scope.localPermissionDataSource = new kendo.data.DataSource();

        var permissionData = {
            displayName: "",
            role: null
        }

        var roleData = {
            text: "",
            value: ""
        }

        var roles = [];
        var permissionList = [];
        var localPermissionList = [];
        $scope.Authority =
        {
            name: "",
            authorityType: "",
            shortName: "",
            fullName: "",
            displayName: ""
        }

        $scope.UserAndGroups =
        {
            authority: null,
            role: ""
        }

        $scope.Permissions = {
            inherited: [],
            isInherited: false,
            canReadInherited: false,
            direct: [],
            settable: []
        };

        function init() {
            $(".loader").show();
            $scope.folder = items.path;
            var nodeId = items.data.split("/").reverse()[0];
            var data = AdministrationApi.getPermissions({ "nodeId": nodeId });

            $q.all([data.$promise]).then(function (response) {
                if (response != null && response.length > 0) {
                    $scope.Permissions = response[0];
                    //if ($scope.Permissions.isInherited) {
                    angular.forEach(response[0].inherited, function (permission) {
                        permissionData = {};
                        permissionData.displayName = permission.authority.displayName;
                        roleData = {};
                        roleData.text = permission.role.replace("Site", "");
                        roleData.value = permission.role;
                        permissionData.role = roleData;
                        permissionList.push(permissionData);
                    });
                    //}
                    $scope.permissionDataSource.read();

                    if ($scope.Permissions.direct != null && response[0].direct.length > 0) {
                        angular.forEach(response[0].direct, function (permission) {
                            permissionData = {};
                            permissionData.displayName = permission.authority.displayName;
                            roleData = {};
                            roleData.text = permission.role.replace("Site", "");
                            roleData.value = permission.role;
                            permissionData.role = roleData;
                            localPermissionList.push(permissionData);
                        });
                    }
                    $scope.localPermissionDataSource.read();

                    if ($scope.Permissions.settable != null && response[0].settable.length > 0) {
                        angular.forEach(response[0].settable, function (role) {
                            roleData = {};
                            roleData.text = role.replace("Site", "");
                            roleData.value = role;
                            roles.push(roleData);
                        });
                    }
                }
                $(".loader").hide();
            });
        };
        $scope.savePermissions = function () {
            $modalInstance.close($scope.FolderModel);
        };
        $scope.closePopup = function () {
            $modalInstance.dismiss("close");
        }

        $scope.permissionDataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success(permissionList);
                }
            },
            pageSize: 10,
            schema: {
                model: {
                    action: "",
                    fields: {
                        displayName: {
                            type: "string", editable: false
                        },
                        role: {
                            defaultValue: { text: "", value: "" }
                        }
                    }
                }
            },
        });

        $scope.permissionGridOptions = {
            dataSource: $scope.permissionDataSource,
            dataBound: function () { },
            sortable: false,
            reorderable: false,
            resizable: true,
            navigatable: false,
            scrollable: false,
            height: 175,
            filterable: false,
            footer: false,
            columns: [
                {
                    field: "displayName", title: "Groups", width: "70%"
                },
                {
                    field: "role.text", title: "Role", width: "30%"
                }
            ]
        }

        $scope.localPermissionDataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success(localPermissionList);
                }
            },
            pageSize: 10,
            schema: {
                model: {
                    action: "",
                    fields: {
                        displayName: {
                            type: "string", editable: false
                        },
                        role: {
                            defaultValue: { text: "", value: "" }
                        }
                    }
                }
            },
        });

        $scope.localPermissionGridOptions = {
            dataSource: $scope.localPermissionDataSource,
            dataBound: function () { },
            sortable: false,
            reorderable: false,
            resizable: true,
            navigatable: false,
            scrollable: false,
            height: 175,
            filterable: false,
            footer: false,
            editable: true,
            columns: [
                {
                    field: "displayName", title: "Users and Groups", width: "50%"
                },
                {
                    field: "role.text", title: "Role", width: "30%", editor: roleDropDown, template: "#=role.text#"
                },
                {
                    field: "", title: "Action", width: "20%"
                }
            ]
        }
        function roleDropDown(container) {
            $('<input required name="role" data-text-field="text" data-value-field="value"/>')
                .appendTo(container)
                .kendoDropDownList({
                    autoBind: false,
                    dataSource: roles
                });
        }

        $scope.inheritPermission = function (action) {
            $scope.Permissions.isInherited = action;
        }
        init();
    }]);
})();