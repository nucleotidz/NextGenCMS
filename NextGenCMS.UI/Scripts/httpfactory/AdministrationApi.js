app.factory('AdministrationApi', ['$resource', 'Global', function ($resource, Global) {
    return $resource('', {}, {
        createUser: {
            method: "POST",
            url: Global.apiuri + "administration/user/create",
            isArray: false,
            param: {}
        },
        getUsers: {
            method: "GET",
            url: Global.apiuri + "administration/users",
            isArray: false,
            param: {}
        },
        getGroups: {
            method: "GET",
            url: Global.apiuri + "administration/groups",
            isArray: false,
            param: {}
        }
    });
}]);


