(function () {
    'use strict';
    app.controller('UserManagementController', ['$scope', '$modal', 'AdministrationApi', '$q', 'Cache',
    function ($scope, $modal, AdministrationApi, $q, Cache) {
        var vm = this;
        vm.userName = Cache.get('userName');
        vm.searchText = "";
        vm.orientation = "vertical";
        vm.editUser = true;
        vm.searchClicked = false;
        vm.addUser = function () {
            var modalInstance = $modal.open({
                backdrop: 'static',
                keyboard: false,
                templateUrl: './Administration/AddUserPopup',
                controller: 'AddUserPopupController'
            });
            modalInstance.result.then(function () { 
            }, function (popupData) {
                if (popupData !== "close") {
                    alert(popupData);
                    if (vm.searchClicked) vm.SearchUser();
                }
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
                        },
                        role: {
                            type: "string", editable: false
                        }
                    }
                }
            },
        });
        vm.SearchUser = function () {
             $(".loader").show();
            vm.searchClicked = true;
            var data;
            if (vm.searchText == "") {
                data = AdministrationApi.getUsers({ "username": vm.userName });
            }
            else {
                data = AdministrationApi.searchUsers({ "searchText": vm.searchText, "username": vm.userName });
            }

            $q.all([data.$promise]).then(function (response) {
                vm.UserData = response[0].people;
                vm.userDataSource.read();
                $(".loader").hide();
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
            },
            {
                field: "role", title: "Role"
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

            var usernames = $scope.users.join(", ");
            var message = "";
            if ($scope.users.length > 1)
                message = "Do you want to delete users " + usernames + " ?";
            else
                message = "Do you want to delete user " + usernames + " ?";
            var modalInstance = $modal.open({
                backdrop: 'static',
                keyboard: false,
                templateUrl: './Administration/DeleteUser',
                controller: 'DeleteUserController',
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
                    var data = AdministrationApi.deleteUsers($scope.users);
                    $q.all([data.$promise]).then(function (response) {
                        if ($scope.users.length > 1) {
                            alert("Users deleted successfuly.");
                        }
                        else if ($scope.users.length == 1) alert("User deleted successfuly.");
                        if (vm.searchClicked) vm.SearchUser();
                        $(".loader").hide();
                    });
                }
            });
        }
    }]);
})();