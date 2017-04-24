(function () {
    'use strict'
    app.factory('WorkFlowAPI', ['$resource', 'Global', function ($resource, Global) {
        return $resource('', {}, {
            GetWorkFlow: {
                method: "GET",
                url: Global.apiuri + "WorkFlow/Get",
                isArray: true                
            }
        });
    }]);
})();
