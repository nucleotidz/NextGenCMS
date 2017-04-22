(function () {
    'use strict';
    app.controller('AddUserPopupController', ['$scope', '$modalInstance', 'AdministrationApi', '$q',//'Cache','$http',
    function ($scope, $modalInstance, AdministrationApi, $q) {
        var vm = this;

        $scope.groupDataSource = new kendo.data.DataSource();
        function init() {
            var data = AdministrationApi.getGroups();
            $q.all([data.$promise]).then(function (response) {
                $scope.groupDataSource = response[0].data;
            });
        };

        $scope.user = {
            userName: "",           //mandatory - the user name for the new user
            firstName: "",          //mandatory - the given Name
            lastName: "",           //mandatory - the family name
            email: "",              //mandatory - the email address
            password: "",
            confirmPassword: "", //optional - the new user's password. If not specified then a value of "password" is used which should be changed as soon as possible.
            disableAccount: false,  //optional - If present and set to "true" the user is created but their account will be disabled.
            quota: -1,              //optional - Sets the quota size for the new user, in bytes.
            groups: [],//,             //optional - Array of group names to assign the new user to.
            groupList: []
            //title: "",              //optional - the title for the new user.
            //organisation: "",       //optional - the organisation the new user belongs to.
            //jobtitle: ""            //optional - the job title of the new user.
        };

        $scope.createUser = function () {
            if (validateForm()) {
                $scope.user.groups = _.pluck($scope.user.groupList, fullName);
                var data = AdministrationApi.createUser($scope.user);
                $q.all([data.$promise]).then(function (response) {
                    if (response !== null && response.length == 1) {
                        var result = response[0];
                        if (result.status === 200) {
                            alert("User created successful.");
                        }
                        else {
                            alert(result.result.message);
                        }
                    }
                    $scope.closePopup();
                });
            }
        };

        $scope.closePopup = function () {
            $modalInstance.dismiss("close");
        };

        function validateForm() {
            if ($scope.user.firstName == undefined || $scope.user.firstName == "") {
                alert("First name is required.");
                return false;
            }
            if ($scope.user.lastName == undefined || $scope.user.lastName == "") {
                alert("Last name is required.");
                return false;
            }
            if ($scope.user.email == undefined || $scope.user.email == "") {
                alert("Email is required.");
                return false;
            }
            if ($scope.user.userName == undefined || $scope.user.userName == "") {
                alert("User name is required.");
                return false;
            }
            if ($scope.user.password == undefined || $scope.user.password == "") {
                alert("Password is required.");
                return false;
            }
            if ($scope.user.confirmPassword == undefined || $scope.user.confirmPassword == "") {
                alert("Confirm Password is required");
                return false;
            }
            //if ($scope.user.password != $scope.user.confirmPassword) {
            //    return false;
            //}
            if ($scope.user.groupList == undefined || $scope.user.groupList == null || $scope.user.groupList.length == 0) {
                alert("Please select atleast one group.");
                return false;
            }

            return true;
        }
        init();
    }]);
})();