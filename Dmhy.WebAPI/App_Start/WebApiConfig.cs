using Dmhy.WebAPI.App_Start;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Dmhy.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            //跨域
            var cors = new EnableCorsAttribute("http://dmhy.amortal.cn", "*", "POST");
            config.EnableCors(cors);

            // Web API 路由
            config.MapHttpAttributeRoutes();


            config.Filters.Add(new DmhyExceptionFilterAttribute());

            #region Json 配置
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            //日期格式化，默认的格式也不好看
            json.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            //json中属性开头字母小写的驼峰命名
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            #endregion

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "v1/{controller}/{action}"
            //defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
