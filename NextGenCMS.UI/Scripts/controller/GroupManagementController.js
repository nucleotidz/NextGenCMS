(function () {
    'use strict';
    app.controller('GroupManagementController', ['$scope', '$modal', 'AdministrationApi', '$q',
    function ($scope, $modal, AdministrationApi, $q) {
        var vm = this;
        vm.searchClicked = false;
        vm.searchText = "";
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
                    display: "No of records is: {2}"
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

        vm.addGroup = function () {
            var modalInstance = $modal.open({
                backdrop: 'static',
                keyboard: false,
                templateUrl: './Administration/AddGroupPopup',
                controller: 'AddGroupPopupController'
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
                data = AdministrationApi.searchGroups({ "searchText": vm.searchText});
            }

            $q.all([data.$promise]).then(function (response) {
                vm.groupData = response[0].data;
                vm.groupDataSource.read();
                $(".loader").hide();
            });
        }
    }]);
})();