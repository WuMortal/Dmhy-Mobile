(function (angular) {
    'use strict';
    var module = angular.module('dmhy.post_list',
        [
            'ngRoute'
        ]
    );

    module.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/:category/:page', {
            templateUrl: 'view/post_list/view.html',
            controller: 'PostListController'
        });
    }]);

    module.controller('PostListController', ["$scope", "$location", "$route", "$http", "$routeParams",
        function ($scope, $location, $route, $http, $routeParams) {
            $scope.category = $routeParams.category;
            $scope.nextPage = true;
            //字幕组Id
            $scope.teamId = $routeParams.teamId;
            //分类Id
            $scope.categoryId = $routeParams.categoryId;
            //搜索关键词
            $scope.keyword = $routeParams.keyword;
            $scope.page = parseInt($routeParams.page);


            //构建查询参数
            var data = {};
            if ($scope.page) {
                data["pageindex"] = $scope.page;
            }
            if ($scope.teamId) {
                data["teamId"] = $scope.teamId;
            }

            if ($scope.categoryId) {
                data["categoryId"] = $scope.categoryId;
            }

            if ($scope.keyword) {
                data["keyword"] = $scope.keyword;
            }

            $scope.loading = true;
            $scope.postList = [];
            //错误消息显示
            $scope.message = "";
            $scope.totalcount = 0;
            $scope.title = "";

            var dmhyApiAddress = "http://dmhyapi.amortal.top/v1/Anime/" + $scope.category;


            //翻页函数 page -页码
            $scope.go = function (page) {

                if (page >= 1)
                    $route.updateParams({page: page});
            }

            //尊重他人请勿删除
            console.log("https://github.com/WuMortal");
            $http({
                method: 'POST',
                url: dmhyApiAddress,
                params: data,
                cache: true
                //data: $.param({pageindex: $scope.page}),
                //headers: {'Content-Type': 'application/x-www-form-urlencoded'}
            }).then(function (res) {
                    $scope.loading = false;
                    if (res.status == 200 && res.data.status == "ok") {
                        $scope.postList = res.data.data;

                        if (res.data.count < 80) {
                            $scope.nextPage = false;
                        }

                        $scope.title = res.data.title;
                        $scope.totalcount = res.data.count;
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
        }
    ]);
})(angular);
