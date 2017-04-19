(function () {
    'use strict';
    app.controller('AddUserPopupController', ['$scope', '$modalInstance', 'AdministrationApi', '$q',
    function ($scope, $modalInstance, AdministrationApi, $q) {
        var vm = this;
        //var token = Cache.get('token');
        $scope.user = {
            userName: "",           //mandatory - the user name for the new user
            firstName: "",          //mandatory - the given Name
            lastName: "",           //mandatory - the family name
            email: "",              //mandatory - the email address
            password: "",           //optional - the new user's password. If not specified then a value of "password" is used which should be changed as soon as possible.
            disableAccount: false,  //optional - If present and set to "true" the user is created but their account will be disabled.
            quota: "",              //optional - Sets the quota size for the new user, in bytes.
            groups: []//,             //optional - Array of group names to assign the new user to.
            //title: "",              //optional - the title for the new user.
            //organisation: "",       //optional - the organisation the new user belongs to.
            //jobtitle: ""            //optional - the job title of the new user.
        };

        $scope.createUser = function () {
            //$http({
            //    method: 'POST',
            //    url: 'http://127.0.0.1:8080/alfresco/s/api/people?alf_ticket=' + token,
            //    data: vm.user
            //}).then(function successCallback(response) {
            //    vm.Documents = response.data.items;
            //}, function errorCallback(response) {
            //    var l = 0;
            //});
            var data = AdministrationApi.createUser($scope.user);
            $q.all([data.$promise]).then(function (response) {
                $modalInstance.dismiss("close");
                //vm.treeData = new kendo.data.HierarchicalDataSource({
                //    data: response[0]
                //});
            });
        };

        $scope.closePopup = function () {
            $modalInstance.dismiss("close");
        };
    }]);
})();