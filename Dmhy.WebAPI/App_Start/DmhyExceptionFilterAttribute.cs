using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Dmhy.WebAPI.App_Start
{
    public class DmhyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        ILog log = LogManager.GetLogger(typeof(DmhyExceptionFilterAttribute));


        public override void OnException(HttpActionExecutedContext context)
        {
            log.ErrorFormat("获取数据异常：" + context.Exception);

            context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);

        }
    }
}