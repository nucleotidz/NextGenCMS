app.factory('AdministrationApi', ['$resource', 'Global', function ($resource, Global) {
    return $resource('', {}, {
        createUser: {
            method: "POST",
            url: Global.apiuri + "administration/user/create",
            isArray: false,
            param: {}
        },
        searchUsers: {
            method: "GET",
            url: Global.apiuri + "administration/users/:searchText",
            isArray: false
        },
        getUsers: {
            method: "GET",
            url: Global.apiuri + "administration/users",
            isArray: false
        },
        getGroups: {
            method: "GET",
            url: Global.apiuri + "administration/groups",
            isArray: false,
            param: {}
        }
    });
}]);


