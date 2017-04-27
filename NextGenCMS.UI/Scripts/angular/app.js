var app = angular.module("NextGenCMS", ["ui.router", "ngResource", "kendo.directives", "ui.bootstrap"]).run(['$rootScope', '$state', '$stateParams',
  function ($rootScope, $state, $stateParams) {
      $rootScope.$state = $state;
      $rootScope.$stateParams = $stateParams;
  }])

