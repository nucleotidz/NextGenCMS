(function () {
    'use strict';
    app.controller('GroupManagementController', ['$scope', '$modal', 'AdministrationApi', '$q',
    function ($scope, $modal, AdministrationApi, $q) {
        var vm = this;
        vm.editData = {
            fullName: "",
            displayName: "",
            editMode: false
        }
        vm.searchClicked = false;
        vm.searchText = "";
        vm.orientation = "vertical";
        vm.singleSelect = true;
        vm.groupData = [];
        vm.groupDataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success(vm.groupData);
                }
            },
            pageSize: 100,
            schema: {
                model: {
                    action: "",
                    fields: {
                        fullName: {
                            type: "string", editable: false
                        },
                        displayName: {
                            type: "string", editable: false
                        }
                    }
                }
            },
        });
        vm.groupGridOptions = {
            dataSource: vm.groupDataSource,
            dataBound: function () {
            },
            sortable: {
                mode: "multiple",
                allowUnsort: true
            },
            reorderable: true,
            resizable: true,
            navigatable: true,
            scrollable: {
                virtual: true
            },
            height: 350,
            selectable: "multiple",
            pageable: {
                numeric: false,
                previousNext: false,
                messages: {
                    empty: "No Records exist",
                    display: "No of records: {2}"
                }
            },
            columns: [
            {
                field: "fullName", title: "Identifier"
            },
              {
                  field: "displayName", title: "Display Name"
              }
            ]
        };

        vm.addGroup = function (obj) {
            var modalInstance = $modal.open({
                backdrop: 'static',
                keyboard: false,
                templateUrl: './Administration/AddGroupPopup',
                controller: 'AddGroupPopupController',
                resolve: {
                    items: function () {
                        return obj;
                    }
                }
            });
            modalInstance.result.then(function () {
            }, function (popupData) {
                if (popupData !== "close") {
                    alert(popupData);
                    if (vm.searchClicked) vm.searchGroup();
                }
            });
        };

        vm.searchGroup = function () {
            $(".loader").show();
            vm.searchClicked = true;
            var data;
            if (vm.searchText === "") {
                data = AdministrationApi.getGroups();
            }
            else {
                data = AdministrationApi.searchGroups({ "searchText": vm.searchText });
            }

            $q.all([data.$promise]).then(function (response) {
                vm.groupData = response[0].data;
                vm.groupDataSource.read();
                $(".loader").hide();
            });
        }

        vm.onSelect = function (evt) {
            var action = evt.item.textContent.trim();
            if (action === "Delete") {
                deleteUser();
            }
            else {
                var entityGrid = $("#groupGrid").data("kendoGrid");
                var selectedItems = entityGrid.select();
                if (selectedItems == null) {
                    evt.preventDefault();
                    return;
                }
                var data = entityGrid.dataItem(selectedItems);
                vm.editData = {};
                vm.editData.fullName = data.fullName;
                vm.editData.displayName = data.displayName;
                if (action === "Edit") {
                    vm.editData.editMode = true;
                    vm.addGroup(vm.editData);
                }
                else if (action === "Manage Users") {
                    vm.ManageUsers();
                }
            }
        };

        vm.ManageUsers = function () {
            var modalInstance = $modal.open({
                backdrop: 'static',
                keyboard: false,
                templateUrl: './Administration/ManageGroupUsersPopup',
                controller: 'ManageGroupUsersPopupController',
                resolve: {
                    items: function () {
                        return vm.editData;
                    }
                }
            });
            modalInstance.result.then(function () {
            }, function (popupData) {
                if (popupData !== "close") {
                    alert(popupData);
                }
            });
        };

        vm.open = function (evt) {
            var entityGrid = $("#groupGrid").data("kendoGrid")
            var selectedItems = entityGrid.select();
            if (selectedItems == null) {
                evt.preventDefault();
                return;
            }
            vm.singleSelect = selectedItems.length == 1;
        };

        function deleteUser() {
            var entityGrid = $("#groupGrid").data("kendoGrid");
            var selectedRows = entityGrid.select();
            $scope.groups = [];
            angular.forEach(selectedRows, function (group) {
                var groupName = entityGrid.dataItem(group).fullName;
                $scope.groups.push(groupName);
            });

            var groupNames = $scope.groups.join(", ");
            var message = "";
            if ($scope.groups.length > 1)
                message = "Do you want to delete groups: " + groupNames + " ?";
            else
                message = "Do you want to delete group: " + groupNames + " ?";
            var modalInstance = $modal.open({
                backdrop: 'static',
                keyboard: false,
                templateUrl: './Administration/DeletePopup',
                controller: 'DeletePopupController',
                resolve: {
                    items: function () {
                        return message;
                    }
                }
            });
            modalInstance.result.then(function () {
            }, function (popupData) {
                if (popupData === "yes") {
                    $(".loader").show();
                    var data = AdministrationApi.deleteGroups($scope.groups);
                    $q.all([data.$promise]).then(function (response) {
                        if ($scope.groups.length > 1) {
                            alert("Groups deleted successfully.");
                        }
                        else if ($scope.groups.length == 1) alert("Group deleted successfully.");
                        if (vm.searchClicked) vm.searchGroup();
                        $(".loader").hide();
                    });
                }
            });
        }
    }]);
})();