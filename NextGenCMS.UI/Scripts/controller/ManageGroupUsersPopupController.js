(function () {
    'use strict';
    app.controller('ManageGroupUsersPopupController', ['$scope', '$modalInstance', 'AdministrationApi', '$q', 'items',
    function ($scope, $modalInstance, AdministrationApi, $q, items) {
        $scope.group = {
            fullName: "",
            displayName: "",
            users: []
        };
        $scope.user = {
            shortName: "",
            displayName: "",
            isExisting: false,
            removed: false
        };
        $scope.search = {
            text: ""
        }

        $scope.searchUsers = [];
        $scope.savedUsers = [];
        //$scope.postUsers = [];

        $scope.searchUserDataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success($scope.searchUsers);
                }
            }
        });

        $scope.selectOptions1 = {
            connectWith: "selected",
            draggable: true,
            dropSources: ["selected"],
            toolbar: {
                position: "right",
                tools: ["transferTo", "transferFrom", "transferAllTo", "transferAllFrom"]
            },
            dataSource: $scope.searchUserDataSource,
            dataTextField: "displayName",
            dataValueField: "shortName"
        };

        $scope.savedUserDataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: function (o) {
                    o.success($scope.savedUsers);
                }
            }
        });

        $scope.selectOptions2 = {
            connectWith: "optional",
            draggable: {
                placeholder: function (element) {
                    return element.clone().css({
                        "opacity": 0.3,
                        "border": "1px dashed #000000"
                    });
                }
            },
            dropSources: ["optional"],
            dataSource: $scope.savedUserDataSource,
            dataTextField: "displayName",
            dataValueField: "shortName",
            add: onAdd,
            remove: onRemove
        };

        function init() {
            $scope.group = items;
            var data = AdministrationApi.getGroupUsers({ "groupname": $scope.group.fullName });
            $q.all([data.$promise]).then(function (response) {
                if (response !== null && response.length === 1) {
                    $scope.savedUsers = response[0].data;
                    angular.forEach($scope.savedUsers, function (user) {
                        user.isExisting = true;
                    });
                    $scope.savedUserDataSource.read();
                }
                $(".loader").hide();
            });
        }

        function onAdd(e) {
            angular.forEach(e.dataItems, function (user) {
                if (user.isExisting !== undefined && user.isExisting) {
                    var obj = _.findWhere($scope.savedUsers, { shortName: user.shortName })
                    _.extend(obj, { removed: false });
                }
                else {
                    $scope.savedUsers.push(user);
                }
            });
        }

        function onRemove(e) {
            angular.forEach(e.dataItems, function (user) {
                if (user.isExisting !== undefined && user.isExisting) {
                    var obj = _.findWhere($scope.savedUsers, { shortName: user.shortName })
                    _.extend(obj, { removed: true });
                }
                else {
                    $scope.savedUsers = _.without($scope.savedUsers, _.findWhere($scope.savedUsers, {
                        shortName: user.shortName
                    }));
                }
            });
        }

        $scope.searchUser = function () {
            $(".loader").show();
            var data;
            if ($scope.search.text == "") {
                data = AdministrationApi.getUsers({ "username": "nouser" });
            }
            else {
                data = AdministrationApi.searchUsers({ "searchText": $scope.search.text, "username": "nouser" });
            }

            $q.all([data.$promise]).then(function (response) {
                if (response !== null && response.length === 1 && response[0].people !== undefined && response[0].people !== null) {
                    $scope.searchUsers = [];
                    angular.forEach(response[0].people, function (user) {
                        var userPresent = $scope.savedUsers.filter(function (savedUser) {
                            return angular.equals(savedUser.shortName, user.userName) && (savedUser.removed === undefined || savedUser.removed === false)
                        }).length === 0;

                        if (userPresent) {
                            $scope.user = {};
                            $scope.user.displayName = user.firstName + (user.lastName === null || user.lastName === "" ? "" : " " + user.lastName);
                            $scope.user.shortName = user.userName;
                            $scope.searchUsers.push($scope.user);
                            $scope.searchUserDataSource.read();
                        }
                    });

                    $(".loader").hide();
                }
            });
        }

        $scope.closePopup = function () {
            $modalInstance.dismiss("close");
        };

        $scope.manageUsers = function () {
            $scope.savedUsers = _.without($scope.savedUsers, _.findWhere($scope.savedUsers, function (user) {
                return user.isExisting !== user.undefined && user.isExisting && (user.removed === undefined || user.removed === false);
            }
            ));

            $scope.group.users = [];
            angular.forEach($scope.savedUsers, function (user) {
                $scope.user = {};
                $scope.user.shortName = user.shortName;
                $scope.user.removed = (user.removed === undefined ? false : user.removed);
                $scope.group.users.push($scope.user);
            });

            if ($scope.group.users.length > 0) {
                var result = AdministrationApi.manageGroupUsers($scope.group);
                $q.all([result.$promise]).then(function (response) {
                    if (response != null && response.length > 0) {
                        alert('users updated');
                    }
                    $(".loader").hide();
                    $modalInstance.dismiss("close");
                });
            }
            else {
                $(".loader").hide();
                alert('no change in data');
            }
        };

        init();
    }]);
})();