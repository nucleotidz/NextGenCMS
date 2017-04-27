(function () {
    'use strict'
    app.factory('SearchAPI', ['$resource', 'Global', function ($resource, Global) {
        return $resource('', {}, {
            SearchFiles: {
                method: "GET",
                url: Global.apiuri + "Search/File/:searchKey/:IsContent",
                isArray: false               
            }
        });
    }]);
})();
