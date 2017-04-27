(function () {
    'use strict';
    app.controller('SearchController', ['SearchAPI', '$q',
    function (SearchAPI, $q) {
        var vm = this;
        vm.searchKey = '';        
        vm.IsContent = false;
        vm.SearchModel = {
            name: "",
            title: "",
            description: "",
            type: ""
        }
        vm.SearchFile = function () {
            var data = SearchAPI.SearchFiles({ "searchKey": vm.searchKey, "IsContent": vm.IsContent
            });
            $q.all([data.$promise]).then(function (response) {
                searchData = response[0].items;              
                vm.searchGridDataSourve.read();
            });
        };

        var searchData = [];

        vm.searchGridDataSourve = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success(searchData);
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
                        type: {
                            type: "string", editable: false
                        },
                        modifiedBy: {
                            type: "string", editable: false
                        },
                        modifiedOn: {
                            type: "string", editable: false
                        },
                        description: {
                            type: "string", editable: false
                        }
                    }
                }
            },
        });
        vm.searchGridOptions = {
            dataSource: vm.searchGridDataSourve,
            dataBound: function () {
            },
            sortable: {
            mode: "multiple",
            allowUnsort: true
            },
            toolbar: ["excel"],
            excel: {
                allPages: true
            },

            groupable: true,
            reorderable: true,
            resizable: true,
            navigatable: true,
                scrollable: true,
                height: 350,
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
                field: "displayName", title: "Name"
            },
              {
                  field: "modifiedBy", title: "Modified By"
              },
            {
                field: "modifiedOn", title: "Modified On", template: "#= kendo.toString(kendo.parseDate(modifiedOn), 'dd MMM yyyy hh:mm') #"
            },
            {
                field: "description", title: "Description"
            },
            {
                field: "type", title: "Type"
            }
            ]
        };

    }]);
})();