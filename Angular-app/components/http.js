'use strict';

(function (angular) {
	var http = angular.module("dmhy.services.http", []);

	http.service("HttpService", ["$window", "$document", function ($window, $document) {

		/*
		*  1. 处理 url 中的回调函数
		*  2. 创建一个 script 标签
		*  3. 挂载回调函数
		*  4. 将 script 标签放在页面中
		*  5. 将 data 转换成 url 字符串的形式
		* */
		this.jsonp = function (url, data, callback) {
			//随机数，用于保证函数的不重复
			var fnSuffix = Math.random().toString().replace(".", "");

			//传入的函数名
			var cbFuncName = 'my_json_cb_' + fnSuffix;

			$window[cbFuncName] = callback;

			var queryString = url.indexOf('?') == -1 ? "?" : "&";

			//构建 url 参数
			for (var key in data) {
				queryString += key + "=" + data[key] + "&";
			}

			queryString += "callback=" + cbFuncName;

			//创建 script 对象
			var scriptElement = $document[0].createElement("script");

			scriptElement.src = url + queryString;

			$document[0].body.appendChild(scriptElement);
		};

	}]);
})(angular);
