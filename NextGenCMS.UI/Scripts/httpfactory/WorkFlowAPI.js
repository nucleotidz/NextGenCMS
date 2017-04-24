(function () {
    'use strict'
    app.factory('WorkFlowAPI', ['$resource', 'Global', function ($resource, Global) {
        return $resource('', {}, {
            SearchFiles: {
                method: "GET",
                url: Global.apiuri + "Search/File/:searchKey",
                isArray: false,
                param: {}
            }
        });
    }]);
})();
