app.factory('AuthenticationAPI', ['$resource', 'Global', function ($resource, Global) {
    return $resource('', {}, {
        AuthenticateUser: {
            method: "POST",
            url: Global.apiuri + "Account/Save",
            isArray: false,
            param: {}
        }
    });
}]);


