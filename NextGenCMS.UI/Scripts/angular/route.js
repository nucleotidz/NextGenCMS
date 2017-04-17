app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/');

    $stateProvider
    .state('Home', {
        url: '/',
        views: {
            'header': {
                templateUrl: 'Home/Header',

            }, 'menu': {
                templateUrl: 'Home/Menu',

            },
            'content': {
                templateUrl: 'Home/Dashboard'
            },
            'toolpane': {
                templateUrl: 'Home/ToolPane'
            }

        }
    }).state('Home.Admin', {      
        views: {           
            'content@': {
                templateUrl: 'Home/Adminstration'
            }       

        }
    });

});