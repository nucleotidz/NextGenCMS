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
            password: "",           //optional - the new user's password. If not specified then a value of "password" is used which should be changed as soon as possible.
            disableAccount: false,  //optional - If present and set to "true" the user is created but their account will be disabled.
            quota: -1,              //optional - Sets the quota size for the new user, in bytes.
            groups: [],//,             //optional - Array of group names to assign the new user to.
            groupList: []
            //title: "",              //optional - the title for the new user.
            //organisation: "",       //optional - the organisation the new user belongs to.
            //jobtitle: ""            //optional - the job title of the new user.
        };

        $scope.createUser = function () {
            //$http({
            //    method: 'POST',
            //    url: 'http://127.0.0.1:8080/alfresco/s/api/people?alf_ticket=' + token,
            //    data: $scope.user
            //}).then(function successCallback(response) {
            //    var x = response.data.items;
            //}, function errorCallback(response) {
            //    var l = 0;
            //});
            if ($scope.user.groupList != null && $scope.user.groupList != undefined && $scope.user.groupList.length > 0)
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
        };

        $scope.closePopup = function () {
            $modalInstance.dismiss("close");
        };

        init();
    }]);
})();