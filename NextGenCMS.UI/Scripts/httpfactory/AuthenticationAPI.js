app.factory('AuthenticationApi', ['$resource', 'Global', function ($resource, Global) {
    return $resource('', {}, {
        Logout: {
            method: "DELETE",
            url: Global.apiuri + "authentication/logout",
            isArray: false,
            param: {}
        }
    });
}]);


