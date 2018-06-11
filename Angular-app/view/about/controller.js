(function (angular) {
    var module = angular.module("dmhy.about",
        [
            'ngRoute'
        ]);

    module.config(["$routeProvider", function ($routeProvider) {
        $routeProvider.when("/about", {
            templateUrl: "view/about/view.html",
        });


    }]);

})(angular);