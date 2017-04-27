app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/');

    $stateProvider
    .state('Home', {
        url: '/',
        views: {
            'header': {
                templateUrl: 'Home/Header',
                controller: 'HeaderController',
                controllerAs: "vm"
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
                templateUrl: 'Administration/Home',
                controller: 'AdministrationController',
                controllerAs: "vm"
            }
        }
    }).state('Home.User', {
        views: {
            'content@': {
                templateUrl: 'Administration/UserManagement',
                controller: 'UserManagementController',
                controllerAs: "vm"
            }
        }
    }).state('Home.Group', {
        views: {
            'content@': {
                templateUrl: 'Administration/GroupManagement',
                controller: 'GroupManagementController',
                controllerAs: "vm"
            }
        }
    }).state('Home.Folder', {
        views: {
            'content@': {
                templateUrl: 'Folder/Home',
                controller: 'FolderController',
                controllerAs: "vm"
            }
        }
    }).state('Home.Search', {
        views: {
            'content@': {
                templateUrl: 'Search/SearchDocument',
                controller: 'SearchController',
                controllerAs: "vm"
            }
        }
    }).state('Home.TaskList', {
        views: {
            'content@': {
                templateUrl: 'Workflow/TaskList',
                controller: 'TaskListController',
                controllerAs: "vm"
            }
        }
    }).state('Home.MyWorkflow', {
        views: {
            'content@': {
                templateUrl: 'Workflow/MyWorkFlow',
                controller: 'WorkflowController',
                controllerAs: "vm"
            }
        }
    });

});