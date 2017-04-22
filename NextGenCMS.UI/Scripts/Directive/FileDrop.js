(function () {
    'use strict';
    app.directive('fileDropzone', function () {
        return {
            restrict: 'A',
            scope: {
                file: '=',
                fileName: '=',
                upload:'&upload'
            },
            link: function (scope, element, attrs) {
                var  processDragOverOrEnter;
                processDragOverOrEnter = function (event) {
                    if (event != null) {
                        event.preventDefault();
                    }
                    event.originalEvent.dataTransfer.effectAllowed = 'copy';
                };                           
                
                element.bind('dragover', processDragOverOrEnter);
                element.bind('dragenter', processDragOverOrEnter);
                return element.bind('drop', function (event) {
                    var file, name, size;
                    if (event != null) {
                        event.preventDefault();
                    }                    
                    file = event.originalEvent.dataTransfer.files[0];
                    name = file.name;                   
                    size = file.size;                   
                    scope.$apply(function () {
                                scope.file = file;
                                 scope.fileName = name;
                            });
                    scope.upload();
                    return false;
                });
            }
        };
    });

}).call(this);