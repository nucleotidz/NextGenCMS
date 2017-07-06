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
        },
        searchGroups: {
            method: "GET",
            url: Global.apiuri + "administration/groups/search/:searchText",
            isArray: false,
            param: {}
        },
        createGroup: {
            method: "POST",
            url: Global.apiuri + "administration/group/create",
            isArray: false,
            param: {}
        },
        deleteGroups: {
            method: "POST",
            url: Global.apiuri + "administration/group/delete",
            isArray: false,
            param: {}
        },
        updateGroup: {
            method: "POST",
            url: Global.apiuri + "administration/group/update",
            isArray: false,
            param: {}
        },
        getGroupUsers: {
            method: "GET",
            url: Global.apiuri + "administration/group/users/get/:groupname",
            isArray: false,
            param: {}
        },
        manageGroupUsers: {
            method: "POST",
            url: Global.apiuri + "administration/group/users/manage",
            isArray: false,
            param: {}
        },
        getPermissions: {
            method: "GET",
            url: Global.apiuri + "administration/permissions/:nodeId",
            isArray: false
        },
        searchUserAndGroups: {
            method: "GET",
            url: Global.apiuri + "administration/search/userandgroups/:searchText",
            isArray: false
        },
        savePermissions: {
            method: "POST",
            url: Global.apiuri + "administration/permissions/save",
            isArray: false,
            param: {}
        }
    });
}]);


