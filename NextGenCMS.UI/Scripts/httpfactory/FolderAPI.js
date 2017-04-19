(function () {
    'use strict'
    app.factory('FolderAPI', ['$resource', 'Global', function ($resource, Global) {
        return $resource('', {}, {
            GetRootFolders: {
                method: "GET",
                url: Global.apiuri + "Folder/Get",
                isArray: true,               
            }
        });
    }]);
})();
