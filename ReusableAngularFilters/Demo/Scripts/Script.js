
var fetch = angular.module('myModule', []);

fetch.controller('myController', ['$scope', '$http', function ($scope, $http) {
    $scope.employees;
    $scope.rowCount;

    $http.get("GetEmployees.php")
        .success(function (data) {
            $scope.employees = data;
            $scope.rowCount = 3;
            $scope.data = "error in fetching data";
        })
        .error(function () {
            $scope.data = "error in fetching data";
        });
}]);
