(function () {
    'use strict';
    app.controller('UploadController', ['$scope', '$modalInstance', 'items', 'Global',
    function ($scope, $modalInstance, items, Global) {
        var FileArray = [];
        $scope.onUpload = function (e) {
        };
        $scope.onSelect = function (e) {
            for (var i = 0; i < e.files.length; i++) {
                var FileObject = {
                    uid: "",
                    File: null
                }
                FileObject.uid = e.files[i].uid;
                FileObject.File = e.files[i];
                FileArray.push(FileObject);
            }
        }
        $scope.onRemove = function (e) {
            if (FileArray.length > 0) {
                FileArray = _.reject(FileArray, function (d) {
                    return d.uid === e.files[0].uid;
                });
            }
        };
        $scope.closePopup = function ()
        {
            $modalInstance.dismiss("close");
        }
        $scope.UploadFile = function () {
            var formdata = new FormData();
            formdata.append("path", items);
            for (var i = 0; i < FileArray.length; i++) {
                formdata.append(FileArray[i].File.name, FileArray[i].File.rawFile);
            }
            var xhr = new XMLHttpRequest();
            xhr.open("POST", Global.apiuri+"File/Upload", false);
            xhr.send(formdata);
            $modalInstance.dismiss("success");
        }
    }]);
})();