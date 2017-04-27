(function () {
    'use strict';
    app.controller('WorkflowController', ['$scope', 'WorkFlowAPI', '$q', 'Profile',
        function ($scope, WorkFlowAPI, $q, Profile) {
            var vm = this;
            var grddata = [];
            var WfData = WorkFlowAPI.GetAllWf({
                "username": Profile.get('Profile').User.userName
            });
            $q.all([WfData.$promise]).then(function (response) {
                if (response != undefined) {
                    grddata = response[0]
                    vm.wfGridDataSource.read();
                }
            });
            vm.wfGridDataSource = new kendo.data.DataSource({
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
                            id: {
                                type: "string",
                                editable: false
                            },
                            name: {
                                type: "string",
                                editable: false
                            },
                            title: {
                                type: "string",
                                editable: false
                            },
                            Priority: {
                                type: "string",
                                editable: false
                            },
                            isActive: {
                                type: "string",
                                editable: false
                            },
                            startDate: {
                                type: "string",
                                editable: false
                            },
                            dueDate: {
                                type: "string",
                                editable: false
                            }

                        }
                    }
                },
            });
            vm.wfGridOption = {
                dataSource: vm.wfGridDataSource,
                dataBound: function () { },
                sortable: {
                    mode: "multiple",
                    allowUnsort: true
                },
                reorderable: true,
                resizable: true,
                navigatable: true,
                scrollable: true,
                height: 470,
                pageable: {
                    numeric: false,
                    previousNext: false,
                    messages: {
                        empty: "No Records exist",
                        display: "No of records is: {2}"
                    }
                },
                selectable: "row",
                filterable: true,
                footer: false,
                columns: [{
                    field: "id",
                    "title": "id",
                    hidden: true
                },
                    {
                        field: "name",
                        title: "name",
                        hidden: true
                    },

                    {
                        field: "title",
                        title: "Title"
                    },
                    {
                        field: "priority",
                        title: "Priority",
                        template: function (dataitem) {
                            if (dataitem.priority.toString() === "1") {
                                return "High"
                            } else if (dataitem.priority.toString() === "2") {
                                return "Medium"
                            } else if (dataitem.priority.toString() === "3") {
                                return "Low"
                            } else {
                                return ""
                            }
                        }
                    },
                    {
                        field: "isActive",
                        title: "Active",
                        template: function (dataitem) {
                            if (dataitem.isActive.toString() === "true") {
                                return "Yes"
                            } else {
                                return "No";
                            }
                        }
                    },
                    {
                        field: "startDate",
                        title: "Start Date",
                        template: "#= kendo.toString(kendo.parseDate(startDate), 'dd MMM yyyy') #"
                    },
                    {
                        field: "dueDate",
                        title: "Due Date",
                        template: "#= kendo.toString(kendo.parseDate(dueDate), 'dd MMM yyyy') #"
                    },

                ]
            }
        }
    ]);
})();