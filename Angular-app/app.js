(function (angular) {
    'use strict';

// Declare app level module which depends on views, and components
    var module = angular.module('dmhy', [
        'ngRoute',
        "dmhy.detailed_post",
        "dmhy.schedule",
        'dmhy.about',
        'dmhy.post_list'
    ]);

    module.config(["$locationProvider", '$compileProvider', '$routeProvider', function ($locationProvider, $compileProvider, $routeProvider) {

        //添加 Href 的地址规则
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|tel|file|sms|magnet|javascript):/);

        $locationProvider.hashPrefix('');
        $routeProvider.otherwise({redirectTo: '/Topics/1'});
    }]);

    module.controller("NavController", [
        "$scope", "$location",
        function ($scope, $location) {
            $scope.keyword = "";

            $scope.search = function () {
                if(!$scope.keyword){
                    return;
                }
                $location.url('/Search/1?keyword=' + $scope.keyword);
                $scope.keyword = "";
            };



            //焦点切换
            $scope.$location = $location;
            $scope.$watch("$location.path()", function (newVal) {
                if (newVal.startsWith('/Topics')) {
                    $scope.type = 'Topics';
                } else if (newVal.startsWith('/Schedule')) {
                    $scope.type = 'Schedule';
                } else if (newVal.startsWith('/about')) {
                    $scope.type = 'about';
                }
            });
        }
    ]);

})(angular);
