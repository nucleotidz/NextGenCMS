﻿(function () {
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
            },
            CancelCheckOut: {
                        method: "GET",
                    url: Global.apiuri + "Folder/CancelCheckout/File/:objectId",
                    isArray: false,
                    params: {}
                },
            CheckInFile: {
                method: "GET",
                url: Global.apiuri + "Folder/Checkin/File/:objectId",
                isArray: false,
                params: {}

            },
            DeleteFolder: {
                method: "POST",
                url: Global.apiuri + "Folder/Delete",
                isArray: false,
                params: {}
            },
            CheckOutCountByUser: {
                method: "GET",
                url: Global.apiuri + "Folder/CheckOutCount/:userName",
                isArray: false               
            }
        });
    }]);
})();
