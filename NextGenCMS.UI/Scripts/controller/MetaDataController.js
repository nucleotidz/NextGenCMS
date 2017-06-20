(function () {
    'use strict';
    app.controller('MetaDataController', ['$scope', '$stateParams', '$state','$modalInstance', 'items','FileAPI','$q',
    function ($scope, $stateParams, $state, $modalInstance, items, FileAPI,$q) {
        var grddata = [];
        $scope.meta = items;
        var Version = {
            nodeRef: $scope.meta[0].nodeRef
        }
        $scope.closePopup = function () {
            $modalInstance.dismiss("close");
        }
        var fileVersions = FileAPI.GetVersion(Version);
        $q.all([fileVersions.$promise]).then(function (response) {
            grddata = response[0]
            $scope.versionGridDataSource.read();
        });

        $scope.versionGridDataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success(grddata);
                }
            },
            pageSize: 1000,
            schema: {
                model: {
                    action: "",
                    fields: {
                        label: {
                            type: "string",
                            editable: false
                        },
                        createdDateISO: {
                            type: "string",
                            editable: false
                        },
                        creator: {
                            defaultValue: { userName: " ", firstName: " ",lastName: " " },
                            editable: false
                        }   

                    }
                }
            },
        });

        $scope.versionGridOption = {
            dataSource: $scope.versionGridDataSource,
            dataBound: function () { },
            sortable: {
                mode: "multiple",
                allowUnsort: true
            },
            reorderable: true,
            resizable: true,
            navigatable: true,
            scrollable: true,
            height: 300,
            pageable: {
                numeric: false,
                previousNext: false,
                messages: {
                    empty: "No Records exist",
                    display: "No of records is: {2}"
                }
            },
            selectable: "row",
            toolbar: ["excel"],
            excel: {
                allPages: true
            },

            groupable: true,
            filterable: true,
            footer: false,
            columns: [
                {
                    field: "label",
                    title: "Version",
                   
                },    
                {
                    field: "createdDateISO",
                    title: "Changed Date",
                    template: "#= kendo.toString(kendo.parseDate(createdDateISO), 'dd MMM yyyy') #",
                    groupHeaderTemplate: function (dataitem) {
                        return kendo.toString(kendo.parseDate(dataitem.value), 'dd MMM yyyy')
                    }

                },
                 {
                     field: "creator.userName",
                     title: "Changed By",
                     template: "#=creator.userName#",

                 },
            ]
        }
    }])
})();
