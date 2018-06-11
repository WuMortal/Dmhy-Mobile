using Dmhy.WebAPI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Dmhy.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // 启动Log4Net日志记录
            log4net.Config.XmlConfigurator.Configure();

            AutofacWebApiConfig.Run();
        }
    }
}
