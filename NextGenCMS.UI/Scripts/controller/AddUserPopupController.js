(function () {
    'use strict';
    app.controller('AddUserPopupController', ['$scope', '$modalInstance', 'AdministrationApi', '$q',//'Cache','$http',
    function ($scope, $modalInstance, AdministrationApi, $q) {
        $scope.groupDataSource = new kendo.data.DataSource();
        function init() {
            $(".loader").show();
            var data = AdministrationApi.getGroups();
            $q.all([data.$promise]).then(function (response) {
                $scope.groupDataSource = response[0].data;
                $(".loader").hide();
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

        $scope.userRole = {
            invitationType: "NOMINATED",
            inviteeUserName: "",
            inviteeRoleName: "",
            inviteeRole: [],
            inviteeFirstName: "",
            inviteeLastName: "",
            inviteeEmail: "",
            serverPath: "",
            acceptURL: "page/accept-invite",
            rejectURL: "page/reject-invite"
        };

        $scope.rolesDataSource =[];
        $scope.rolesDataSource.push({
            "text": "Manager", "value" : "SiteManager"
        });
        $scope.rolesDataSource.push({
            "text": "Collaborator", "value": "SiteCollaborator"
        });
        $scope.rolesDataSource.push({
            "text": "Contributor", "value": "SiteContributor"
        });
        $scope.rolesDataSource.push({
            "text": "Consumer", "value": "SiteConsumer"
        });

        $scope.customOptions = {
               dataSource: $scope.rolesDataSource,
               dataTextField: "text",
               dataValueField: "value"
        };

        $scope.createUser = function () {
            $(".loader").show();
            if (validateForm()) {
                $scope.user.groups = _.pluck($scope.user.groupList, "fullName");
                $scope.userRole.inviteeUserName =$scope.user.userName ;
                $scope.userRole.inviteeFirstName= $scope.user.firstName    ;   
                $scope.userRole.inviteeLastName=$scope.user.lastName;
                $scope.userRole.inviteeEmail =$scope.user.email;
                $scope.userRole.inviteeRoleName = $scope.userRole.inviteeRole.value;

                 $scope.createUserRequest ={
                    User: $scope.user,
                    UserRole: $scope.userRole
                };
                var data = AdministrationApi.createUser($scope.createUserRequest);
                $q.all([data.$promise]).then(function (response) {
                    if (response !== null && response.length == 1) {
                        var result = response[0];
                        if (result.status === 200) {
                            $modalInstance.dismiss("User created successful.");
                        }
                        else {
                            $modalInstance.dismiss(result.result.message);
                        }
                    }
                    $(".loader").hide();
                });
            }
            else $(".loader").hide();
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
            if ($scope.user.password != $scope.user.confirmPassword) {
                alert("Password do not match.");
                return false;
            }
            if ($scope.userRole.inviteeRole == undefined || $scope.userRole.inviteeRole == null || $scope.userRole.inviteeRole.length == 0) {
                alert("Please select role.");
                return false;
            }

            return true;
        }
        init();
    }]);
})();