(function () {
    'use strict';
    app.factory('Cache', function ($cacheFactory) {
        return $cacheFactory('token');
    })
})();