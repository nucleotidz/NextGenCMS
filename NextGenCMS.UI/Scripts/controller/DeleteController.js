(function () {
    'use strict';
    app.controller('DeleteController', ['$scope', '$rootScope', 'FolderAPI', 'FileAPI', '$q', '$modal', '$modalInstance', 'items',
function ($scope, $rootScope, FolderAPI, FileAPI, $q, $modal, $modalInstance, items) {
    $scope.message = ""
    $scope.Delete=function(){
        if (items.entity.trim() === "file") {
            var FilePath = { "path": items.path + "/" + items.data };
            var data = FileAPI.DeleteFiles(FilePath);
            $q.all([data.$promise]).then(function (response) {
                if (response[0].overallSuccess) {
                    $modalInstance.dismiss("success");
                }
            });
        }
    }
    $scope.closePopup = function () {
        $modalInstance.dismiss("close");
    }
    function bind() {              
        if(items.entity.trim()==="folder"){
            $scope.message="Deleting folder "+items.data.name +" will delete all its child and files into them , do you want to continue ?"
        }
        else if (items.entity.trim() === "file") {
            $scope.message = "Do you want to delete file " + items.data +" ?" 
        }        
    }
    bind();
}])
})();