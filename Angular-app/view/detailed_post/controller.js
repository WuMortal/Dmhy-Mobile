(function (angular) {
    var module = angular.module("dmhy.detailed_post",
        [
            "ngRoute",
            'ngSanitize'
        ]);

    module.config(["$routeProvider", function ($routeProvider) {
        $routeProvider.when('/Detailed/:name', {
            templateUrl: 'view/detailed_post/view.html',
            controller: 'DetailedController'
        });
    }]);

    module.controller("DetailedController",
        [
            "$scope",
            "$http",
            "$routeParams",
            '$location',
            '$anchorScroll',
        function ($scope, $http, $routeParams, $location, $anchorScroll) {
            $scope.postDetailed = {};
            $scope.message = "";
            $scope.loading = true;
            var dmhyApiAddress = "http://dmhyapi.amortal.top/v1/Anime/Detailed";

            $scope.goDownload = function () {
                $location.hash("description-end");
                $anchorScroll();
            };

            //非跨域
            $http({
                method: 'POST',
                url: dmhyApiAddress,
                params: {name: $routeParams.name},
                cache: true
                //data: $.param({pageindex: $scope.page}),
                //headers: {'Content-Type': 'application/x-www-form-urlencoded'}
            }).then(function (res) {
                    $scope.loading = false;
                    if (res.status == 200 && res.data.status == "ok") {
                        $scope.postDetailed = res.data.data;
                    } else if (res.data.status == "not data") {
                        $scope.message = res.data.errorMsg;
                    } else {
                        $scope.message = '获取数据失败，错误信息:' + res.statusText;
                    }
                }, function (err) {
                    $scope.loading = false;
                    $scope.message = '获取数据失败，错误信息:' + err.statusText;
                }
            );

        }]).
    filter('to_trusted',['$sce',function ($sce) {
        return function (text) {
            return $sce.trustAsHtml(text);
        }
    }]);

})(angular);