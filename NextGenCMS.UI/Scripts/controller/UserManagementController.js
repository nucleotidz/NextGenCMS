(function () {
    'use strict';
    app.controller('UserManagementController', ['$scope', '$modal', 'AdministrationApi', '$q',
    function ($scope, $modal, AdministrationApi, $q) {
        var vm = this;
        vm.userName = Cache.get('userName');
        vm.searchText = "";
        vm.orientation = "vertical";
        vm.editUser = true;
        vm.addUser = function () {
            var modalInstance = $modal.open({
                backdrop: 'static',
                keyboard: false,
                templateUrl: './Administration/AddUserPopup',
                controller: 'AddUserPopupController'
            });
        };

        vm.UserData = [];
        vm.userDataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success(vm.UserData);
                }
            },
            pageSize: 1000,
            schema: {
                model: {
                    action: "",
                    fields: {
                        firstName: {
                            type: "string", editable: false
                        },
                        userName: {
                            type: "string", editable: false
                        },
                        lastName: {
                            type: "string", editable: false
                        },
                        email: {
                            type: "string", editable: false
                        },
                        jobtitle: {
                            type: "string", editable: false
                        }
                    }
                }
            },
        });
        vm.SearchUser = function () {
            var data;
            if (vm.searchText == "") {
                data = AdministrationApi.getUsers(vm.userName);
            }
            else {
                data = AdministrationApi.searchUsers({ "searchText": vm.searchText, "username": vm.userName });
            }

            $q.all([data.$promise]).then(function (response) {
                vm.UserData = response[0].people;
                vm.userDataSource.read();
            });
        }

        vm.userGridOptions = {
            dataSource: vm.userDataSource,
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
            selectable: "multiple",
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
                field: "firstName", title: "First Name"
            },
              {
                  field: "lastName", title: "Last Name"
              },
            {
                field: "userName", title: "User Name"
            },
            {
                field: "email", title: "Email Id"
            },
            {
                field: "jobtitle", title: "Job Title"
            }
            ]
        };

        vm.onSelect = function (evt) {
            var action = evt.item.textContent.trim();
            if (action === "Delete") {
                deleteUser();
            }
        }
        vm.open = function (evt) {
            var entityGrid = $("#userGrid").data("kendoGrid")
            var selectedItems = entityGrid.select();
            if (selectedItems == null) {
                evt.preventDefault();
                return;
            }
            vm.editUser = selectedItems.length == 1;
        }

        function deleteUser() {
            var entityGrid = $("#userGrid").data("kendoGrid");
            var selectedRows = entityGrid.select();
            $scope.users = [];
            angular.forEach(selectedRows, function (user) {
                var username = entityGrid.dataItem(user).userName;
                $scope.users.push(username);
            });

            var data = AdministrationApi.deleteUsers($scope.users);

            $q.all([data.$promise]).then(function (response) {
                if ($scope.users.length > 1) {
                    alert("Users deleted successfuly.");
                    vm.SearchUser();
                }
                else if ($scope.users.length == 1) alert("User deleted successfuly.");

            });
        }
    }]);
})();