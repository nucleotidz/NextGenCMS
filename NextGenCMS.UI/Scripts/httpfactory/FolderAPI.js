(function () {
    'use strict'
    app.factory('FolderAPI', ['$resource', 'Global', function ($resource, Global) {
        return $resource('', {}, {
            GetRootFolders: {
                method: "GET",
                url: Global.apiuri + "Folder/Get",
                isArray: true,               
            },
            GetSubFolderFolders: {
                method: "POST",
                url: Global.apiuri + "Folder/Get/SubFolder",
                isArray: true,
                params:{}
            },
            CreateFolder: {
                method: "POST",
                url: Global.apiuri + "Folder/Create",
                isArray: false,
                params: {}
            },
            CreateSubFolder: {
                method: "POST",
                url: Global.apiuri + "Folder/Create/SubFolder",
                isArray: false,
                params: {}
            },
            CheckOutFile: {
                method: "POST",
                url: Global.apiuri + "Folder/Checkout/File/",
                isArray: false,
                params: {}
            }
        });
    }]);
})();
