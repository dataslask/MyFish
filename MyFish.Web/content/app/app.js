var app = angular.module('myFishApp', ['ngRoute']);

app.factory('_', ['$window', function ($window) {

    var _ = $window._;

    //delete $window._;

    return _;

}]).run(['_', function(_) {}]);

app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/board', {
            templateUrl: 'app/board.html',
            controller: 'BoardController',
            controllerAs: 'board',
        }).       
        otherwise({
            redirectTo: '/board'
        });
  }]);

app.controller('BoardController', ['$http', '_',
  function ($http, _) {

      var self = this;

      self.pieces = null;
      
      $http.get("api/init").success(function(data) {
          self.pieces = data.pieces;
      });
  }]);