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
            url: Global.apiuri + "administration/users/:searchText/:username",
            isArray: false
        },
        getUsers: {
            method: "GET",
            url: Global.apiuri + "administration/users/:username",
            isArray: false
        },
        getUser: {
            method: "GET",
            url: Global.apiuri + "administration/user/:username",
            isArray: false
        },
        deleteUsers: {
            method: "POST",
            url: Global.apiuri + "administration/user/delete",
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


