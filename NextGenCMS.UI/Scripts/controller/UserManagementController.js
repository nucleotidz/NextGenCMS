(function () {
    'use strict';
    app.controller('UserManagementController', ['$scope', '$modal', 'AdministrationApi', '$q',
    function ($scope, $modal, AdministrationApi, $q) {
        var vm = this;
        vm.searchText = "";
        vm.addUser = function () {
            var modalInstance = $modal.open({
                backdrop: 'static',
                keyboard: false,
                templateUrl: './Administration/AddUserPopup',
                controller: 'AddUserPopupController'
            });
        };

        vm.UserData = [];
        vm.userDataSourve = new kendo.data.DataSource({
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
                data = AdministrationApi.getUsers();
            }
            else {
                data = AdministrationApi.searchUsers({ "searchText": vm.searchText });
            }

            $q.all([data.$promise]).then(function (response) {
                vm.UserData = response[0].people;
                vm.userDataSourve.read();
            });
        }

        vm.userGridOptions = {
            dataSource: vm.userDataSourve,
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

    }]);
})();