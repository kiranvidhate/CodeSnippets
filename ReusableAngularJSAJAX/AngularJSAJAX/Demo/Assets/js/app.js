var App = angular.module('App', []);

App.controller('TodoCtrl', function ($scope, $http) {
    $http.get('Data/todos.txt')
         .then(function (res) {
             $scope.todos = res.data;
         });
});