var app = angular.module('validation', []);
app.controller('SignUpController', function ($scope, $http) {
    $scope.person = {};
    $scope.sendForm = function () {
        $http({
            method: 'POST',
            url: '/Account/SignUp',
            data: $scope.person
        }).success(function (data, status, headers, config) {
            $scope.message = '';
            $scope.errors = [];
            if (data.success === false) {
                $scope.errors = data.errors;
            }
            else {
                $scope.message = 'Saved Successfully';
                $scope.person = {};
            }
        }).error(function (data, status, headers, config) {
            $scope.errors = [];
            $scope.message = 'Unexpected Error';
        });
    };
});