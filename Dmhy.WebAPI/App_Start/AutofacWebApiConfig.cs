using Autofac;
using Autofac.Integration.WebApi;
using Dmhy.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Dmhy.WebAPI.App_Start
{
    public class AutofacWebApiConfig
    {
        public static void Run()
        {
            SetAutofacWebApi();
        }

        private static void SetAutofacWebApi()
        {
            #region  AutoFac自动属性注入
            var builder = new ContainerBuilder();

            HttpConfiguration config = GlobalConfiguration.Configuration;

            // Register API controllers using assembly scanning.
            Assembly[] assemblies = new Assembly[] { Assembly.Load("Dmhy.Service") };

            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly).PropertiesAutowired();

            //过滤 条件
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => !type.IsAbstract && typeof(IServiceSupport).IsAssignableFrom(type))     //type2是否实现了type1
                .AsImplementedInterfaces().PropertiesAutowired();

            //注册系统级别的 DependencyResolver
            var container = builder.Build();
            // Set the WebApi dependency resolver.
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            #endregion
        }
    }
}