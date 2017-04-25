(function () {
    'use strict';
    app.factory('Cache', function ($cacheFactory) {
        return $cacheFactory('token');
    })
    app.factory('Profile', function ($cacheFactory) {
        return $cacheFactory('Profile');
    });
})();