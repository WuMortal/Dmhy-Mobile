(function (angular) {
    var module = angular.module("dmhy.schedule",
        [
            "ngRoute"
        ]);
    module.config(["$routeProvider", function ($routeProvider) {
        $routeProvider.when('/Schedule', {
            templateUrl: 'view/schedule/view.html',
            controller: 'ScheduleController'
        });
    }]);

    module.controller("ScheduleController",
        [
            "$scope",
            "$http",
            '$location',
            function ($scope, $http, $location) {
                var mydate = new Date();
                var myddy = mydate.getDay();//获取存储当前日期

                $scope.schedule = {};
                $scope.currentDay = myddy;
                $scope.currentDayData = [];

                $scope.loading = true;

                var dmhyApiAddress = "http://dmhyapi.amortal.top/v1/Anime/Schedule";

                $scope.select = function (day) {
                    $scope.currentDay = day;
                    $scope.currentDayData = $scope.schedule[day];
                };

                //非跨域
                $http({
                    method: 'POST',
                    url: dmhyApiAddress,
                    cache: true
                    //data: $.param({pageindex: $scope.page}),
                    //headers: {'Content-Type': 'application/x-www-form-urlencoded'}
                }).then(function (res) {
                        $scope.loading = false;
                        if (res.status == 200 && res.data.status == "ok") {
                            $scope.schedule = res.data.data;

                            $scope.select($scope.currentDay);

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