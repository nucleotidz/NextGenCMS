(function () {
    'use strict'
    app.factory('FileAPI', ['$resource', 'Global', function ($resource, Global) {
        return $resource('', {}, {           
            GetFiles: {
                method: "POST",
                url: Global.apiuri + "File/Get/",
                isArray: false,
                params: {}
            },
            DeleteFiles: {
                method: "POST",
                url: Global.apiuri + "File/Delete/",
                isArray: false,
                params: {}
            },
            GetVersion: {
                method: "POST",
                url: Global.apiuri + "File/Get/Version",
                isArray: true,
                params: {}
            }
           
        });
    }]);
})();
