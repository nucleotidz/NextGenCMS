(function () {
    'use strict';
    app.controller('UploadController', ['$scope', '$modalInstance',
    function ($scope, $modalInstance) {
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

        }
    }]);
})();