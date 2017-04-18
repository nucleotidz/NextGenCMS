(function () {
    'use strict'
    angular.module('NextGenCMS').factory('TokenInjector', ['Cache', function (Cache) {
        var TokenInjector = {
            request: function (config) {
                config.headers['xsrf-token'] = Cache.get("token");
                return config;
            }
        };
        return TokenInjector;
    }])
    angular.module('NextGenCMS').config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push('TokenInjector');

    }]);
})();
   
  