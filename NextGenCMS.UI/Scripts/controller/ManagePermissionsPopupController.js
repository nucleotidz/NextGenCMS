(function () {
    'use strict';
    app.controller('ManagePermissionsPopupController', ['$scope', '$modalInstance', 'AdministrationApi', '$q', 'items', 'UserProfile',
    function ($scope, $modalInstance, AdministrationApi, $q, items, UserProfile) {
        $scope.permissionDataSource = new kendo.data.DataSource();
        $scope.localPermissionDataSource = new kendo.data.DataSource();
        $scope.showSearchGrid = false;
        $scope.noRecordExist = false;
        $scope.isWindowOpen = false;
        $scope.showRoleInfo = false;
        $scope.showStartSearch = false;
        $scope.errorSearch = false;
        $scope.search = {
            text: ""
        }
        var permissionData = {
            displayName: "",
            name: "",
            role: null
        }

        var roleData = {
            text: "",
            value: ""
        }

        var roles = [];
        $scope.defaultPermissionList = [];
        $scope.localPermissionList = [];
        $scope.PostDataList = [];
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

        $scope.PostPermissions = {
            authority: "",
            role: "",
            remove: null,
            newlyAdded: false
        };

        $scope.PostData = {
            isInherited: false,
            permissions: [],
            nodeId: ""
        };


        function init() {
            $(".loader").show();
            $scope.folder = items.path;
            $scope.PostData.nodeId = items.data.split("/").reverse()[0];
            var data = AdministrationApi.getPermissions({ "nodeId": $scope.PostData.nodeId });

            $q.all([data.$promise]).then(function (response) {
                if (response != null && response.length > 0) {
                    $scope.Permissions = response[0];
                    angular.forEach(response[0].inherited, function (permission) {
                        permissionData = {};
                        permissionData.displayName = permission.authority.displayName;
                        roleData = {};
                        roleData.text = permission.role.replace("Site", "");
                        roleData.value = permission.role;
                        permissionData.role = roleData;
                        $scope.defaultPermissionList.push(permissionData);
                    });
                    $scope.permissionDataSource.read();

                    if ($scope.Permissions.direct != null && response[0].direct.length > 0) {
                        angular.forEach(response[0].direct, function (permission) {
                            permissionData = {};
                            permissionData.displayName = permission.authority.displayName;
                            permissionData.name = permission.authority.name;
                            roleData = {};
                            roleData.text = permission.role.replace("Site", "");
                            roleData.value = permission.role;
                            permissionData.role = roleData;
                            $scope.localPermissionList.push(permissionData);

                            //create a list to be sent back on submit
                            $scope.PostPermissions = {};
                            $scope.PostPermissions.authority = permission.authority.name;
                            $scope.PostPermissions.role = permission.role;
                            $scope.PostPermissions.newlyAdded = false;
                            $scope.PostPermissions.remove = false;
                            $scope.PostData.permissions.push($scope.PostPermissions);
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
            $(".loader").show();
            $scope.PostData.isInherited = $scope.Permissions.isInherited;
            var data = AdministrationApi.savePermissions($scope.PostData);
            $q.all([data.$promise]).then(function (response) {
                $(".loader").hide();
                $scope.closePopup();
            });
        };

        $scope.closePopup = function () {
            $modalInstance.dismiss("close");
        }

        $scope.permissionDataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success($scope.defaultPermissionList);
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
            height: 150,
            filterable: false,
            footer: false,
            columns: [
                {
                    field: "displayName", title: "Groups", width: "50%"
                },
                {
                    field: "role.text", headerTemplate: 'Role <a href ng-click="showRoles(0);"><span class="glyphicon glyphicon-info-sign fontRole"></span></a>', width: "50%"
                }
            ]
        }

        $scope.localPermissionDataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success($scope.localPermissionList);
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
            sortable: false,
            reorderable: false,
            resizable: true,
            navigatable: false,
            scrollable: false,
            filterable: false,
            footer: false,
            editable: true,
            noRecords: true,
            messages: {
                noRecords: "No permissions set"
            },
            columns: [
                {
                    field: "displayName", title: "Users and Groups", width: "50%"
                },
                {
                    field: "role.text", headerTemplate: 'Role <a href ng-click="showRoles(1);"><span class="glyphicon glyphicon-info-sign fontRole"></span></a>', width: "30%", editor: roleDropDown, template: "#=role.text#"
                },
                {
                    field: "", title: "Action",
                    template: "<a href='\\#' ng-click='deleteItems(this)'><span class='glyphicon glyphicon-remove'></span> Delete</a>"
                }
                //{
                //    command: [{
                //        name: "Delete", text: "Delete", //visible: hideDelete,
                //        click: function (e) {
                //            //get the row for deletion
                //            var tr = angular.element(e.target).select().closest("tr");
                //            var data = this.dataItem(tr);
                //            $scope.localPermissionDataSource.remove(data);

                //            $scope.localPermissionList = _.without($scope.localPermissionList, _.findWhere($scope.localPermissionList, {
                //                name: data.name
                //            }));

                //            //get the object from the list
                //            var permission = _.where($scope.PostData.permissions, { authority: data.name, newlyAdded: false, remove: false });

                //            //remove the object from the list
                //            $scope.PostData.permissions = _.without($scope.PostData.permissions, _.findWhere($scope.PostData.permissions, {
                //                authority: data.name, remove: false
                //            }));

                //            if (permission.length > 0) {
                //                //if it was the existing object- add again with flag remove as true
                //                $scope.PostPermissions = {};
                //                $scope.PostPermissions.authority = data.name;
                //                $scope.PostPermissions.role = data.role.value;
                //                $scope.PostPermissions.remove = true;
                //                $scope.PostPermissions.newlyAdded = false;
                //                $scope.PostData.permissions.push($scope.PostPermissions);
                //            }

                //            e.preventDefault();
                //        }
                //    }],
                //    attributes: { style: "text-align:center" }, title: "Action", width: '8%'
                //}
            ]
        }

        $scope.showRoles = function (val) {
            $scope.showRoleInfo = true;
            if (val === 1) {
                $('#roles').addClass('rolesTop').removeClass('rolesBtm');
            }
            else {
                $('#roles').addClass('rolesBtm').removeClass('rolesTop');
            }
        };

        $scope.closeRoleWindow = function () {
            $scope.showRoleInfo = false;
        };

        $scope.deleteItems = function (e) {
            //get the row for deletion
            var data = e.dataItem;
            $scope.localPermissionDataSource.remove(data);

            $scope.localPermissionList = _.without($scope.localPermissionList, _.findWhere($scope.localPermissionList, {
                name: data.name
            }));

            //get the object from the list
            var permission = _.where($scope.PostData.permissions, { authority: data.name, newlyAdded: false, remove: false });

            //remove the object from the list
            $scope.PostData.permissions = _.without($scope.PostData.permissions, _.findWhere($scope.PostData.permissions, {
                authority: data.name, remove: false
            }));

            if (permission.length > 0) {
                //if it was the existing object- add again with flag remove as true
                $scope.PostPermissions = {};
                $scope.PostPermissions.authority = data.name;
                $scope.PostPermissions.role = data.role.value;
                $scope.PostPermissions.remove = true;
                $scope.PostPermissions.newlyAdded = false;
                $scope.PostData.permissions.push($scope.PostPermissions);
            }

            return;
        };
        //function hideDelete(dataItem) {
        //    return ((dataItem.name !== "GROUP_site_" + siteName + "_SiteManager") && (dataItem.role.text !== "SiteManager"));
        //}

        function roleDropDown(container) {
            $('<input required name="role" data-text-field="text" data-value-field="value"/>')
                .appendTo(container)
                .kendoDropDownList({
                    autoBind: false,
                    dataSource: roles,
                    change: onChange
                });
        }

        function onChange(e) {
            //var obj = e.sender.value();
            var grid = $('#localPermissionGrid').data("kendoGrid");
            var selectedItem = grid.dataItem(grid.tbody.find("tr.k-grid-edit-row"));

            //get the object from the list
            var permission = _.where($scope.PostData.permissions, { authority: selectedItem.name, newlyAdded: false, remove: false });

            //remove the object from the list
            $scope.PostData.permissions = _.without($scope.PostData.permissions, permission[0]);

            if (permission.length > 0) {
                //if it was the existing object- add again with flag remove as true
                $scope.PostPermissions = {};
                $scope.PostPermissions.authority = permission[0].authority;
                $scope.PostPermissions.role = permission[0].role;
                $scope.PostPermissions.remove = true;
                $scope.PostPermissions.newlyAdded = false;
                $scope.PostData.permissions.push($scope.PostPermissions);
            }
            else {
                $scope.PostData.permissions = _.without($scope.PostData.permissions, _.findWhere($scope.PostData.permissions, {
                    authority: selectedItem.name, newlyAdded: true, remove: false
                }));
            }

            $scope.PostPermissions = {};
            $scope.PostPermissions.authority = selectedItem.name;
            $scope.PostPermissions.role = selectedItem.role.value;
            $scope.PostPermissions.remove = false;
            $scope.PostPermissions.newlyAdded = true;
            $scope.PostData.permissions.push($scope.PostPermissions);
        }

        $scope.inheritPermission = function (action) {
            if (action) {
                // "Are you sure you do not want to inherit permissions? Only local permissions will apply to this document/folder."

            }
            $scope.Permissions.isInherited = action;
            $scope.localPermissionDataSource.read();
        }

        $scope.addUserGroup = function () {
            if ($scope.isWindowOpen) {
                $scope.isWindowOpen = false;
                $scope.closeSearchWindow();
                $scope.showStartSearch = false;
            }
            else {

                $scope.isWindowOpen = true;
                $scope.showSearchGrid = true;
                $scope.noRecordExist = false;
                $scope.showStartSearch = true;
            }
        };

        $scope.searchUser = function () {
            $scope.errorSearch = false;
            $scope.showStartSearch = false;
            $scope.noRecordExist = false;
            if ($scope.search.text === "") {
                $scope.source = new kendo.data.DataSource({
                    data: [],
                    pageSize: 10
                });
                $scope.errorSearch = true;
                return;
            }

            var data = AdministrationApi.searchUserAndGroups({ "searchText": $scope.search.text });
            var userAndGroups = [];
            $q.all([data.$promise]).then(function (response) {
                if (response != null && response != undefined && response.length > 0 && response[0].authorities != null && response[0].authorities.length > 0) {
                    angular.forEach(response[0].authorities, function (_authority) {
                        var userPresent = $scope.localPermissionList.filter(function (permission) {
                            return angular.equals(permission.name, _authority.fullName)
                        }).length == 0;

                        var siteGroups = true;
                        if ($scope.Permissions.isInherited) {
                            siteGroups = $scope.Permissions.inherited.filter(function (inherited) {
                                return angular.equals(inherited.authority.name, _authority.fullName)
                            }).length == 0;
                        }

                        if (userPresent && siteGroups) {
                            userAndGroups.push(_authority);
                        }
                    });
                }
                $scope.noRecordExist = (userAndGroups.length === 0);
                $scope.source = new kendo.data.DataSource({
                    data: userAndGroups,
                    pageSize: 10
                });
            });
        }

        $scope.closeSearchWindow = function () {
            $scope.showSearchGrid = false;
            $scope.search.text = "";
            $scope.source = new kendo.data.DataSource({
                data: [],
                pageSize: 10
            });
        }

        $scope.addItem = function (e) {
            $("div[data-uid='" + e.uid + "']").hide();
            //$("div[data-uid='" + e.uid + "']").find("a#remove").show();

            $scope.localPermissionList.push(setPermissionData(e));
            $scope.localPermissionDataSource.read();

            ////add in the post data list
            $scope.PostPermissions = {};
            $scope.PostPermissions.authority = e.fullName;
            $scope.PostPermissions.role = e.authorityType === "GROUP" ? "SiteContributor" : e.role;
            $scope.PostPermissions.newlyAdded = true;
            $scope.PostPermissions.remove = false;
            $scope.PostData.permissions.push($scope.PostPermissions);
        }

        $scope.removeItem = function (e) {
            $("div[data-uid='" + e.uid + "']").find("a#add").show();
            $("div[data-uid='" + e.uid + "']").find("a#remove").hide();

            $scope.localPermissionList = _.without($scope.localPermissionList, _.findWhere($scope.localPermissionList, {
                name: e.fullName
            }));

            $scope.localPermissionDataSource.read();

            //delete from the post data list
            $scope.PostData.permissions = _.without($scope.PostData.permissions, _.findWhere($scope.PostData.permissions, {
                authority: e.fullName
            }));
        }

        function setPermissionData(e) {
            permissionData = {};
            permissionData.displayName = e.displayName;
            permissionData.name = e.fullName;
            roleData = {};
            roleData.text = e.authorityType === "GROUP" ? "Contributor" : ((e.role.text === undefined) ? e.role.replace("Site", "") : e.role.text);
            roleData.value = e.authorityType === "GROUP" ? "SiteContributor" : ((e.role.value === undefined) ? e.role : e.role.value);
            permissionData.role = roleData;
            return permissionData;
        }
        init();
    }]);
})();