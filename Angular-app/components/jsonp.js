(function (window, document) {
	'use strict'

	/*
	* ----------JavaScript 实现 ajax 跨域----------
	* 1. 创建一个随机的函数名称
	* 2. 将函数绑定到回调函数上
	* 3. 遍历传入的 data 构建 queryString
	* 4. 创建 script 元素对象
	* 5. 给创建好的 script 对象 src 属性赋值
	* 6. 将对象追加到 body 中
	* 7. 赋值为全局对象
	* */
	var jsonp = function (url, data, callback) {
		//随机数，用于保证函数的不重复
		var fnSuffix = Math.random().toString().replace(".", "");

		//传入的函数名
		var cbFuncName = 'my_json_cb_' + fnSuffix;

		window[cbFuncName] = callback;

		var queryString = url.indexOf('?') == -1 ? "?" : "&";

		//构建 url 参数
		for (var key in data) {
			queryString += key + "=" + data[key] + "&";
		}

		queryString += "callback=" + cbFuncName;

		//创建 script 对象
		var scriptElement = document.createElement("script");

		scriptElement.src = url + queryString;

		document.body.appendChild(scriptElement);

	};

	window.$jsonp = jsonp;
})(window, document);
