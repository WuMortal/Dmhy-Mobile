(function (angular) {

	//创建模块
	angular.module("moviecat.auto_focus", [])

	//创建自定义指令
		.directive("autoFocus", ["$location", function ($location) {
			//获取当前路径
			var path = $location.path();

			return {
				restrict: 'A',
				link: function ($scope, iElm, iAttrs, controller) {
					//获取 a 标签 地址链接
					var aLink = iElm.children().attr("href");

					//获取 类型
					var type = aLink.replace(/#(\/.+?)\/\d+/, '$1');

					//判断地址是哪个 type
					if (path.startsWith(type)) {
						iElm.addClass("active");
					}

					iElm.on("click", function () {
						iElm.parent().children().removeClass("active");
						iElm.addClass("active");
					});
				}
			};

		}]);
})(angular);
