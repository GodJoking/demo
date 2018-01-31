using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace APP_UserManage.Controllers
{
    public class BaseController : ApiController
    {
        protected string RequestIP { get; set; }

        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            IEnumerable<string> ips;
            if(controllerContext.Request.Headers.TryGetValues("X-Forwarded-For",out ips))
            {
                RequestIP = ips.FirstOrDefault();
            }
            LogHelper.Info("请求信息", "请求IP：" + RequestIP + "\r\n请求url：" + controllerContext.Request.RequestUri);

            Task<HttpResponseMessage> task = base.ExecuteAsync(controllerContext, cancellationToken);

            LogHelper.Info("响应信息", task.Result.Content.ReadAsStringAsync().Result);

            return task;
        }
    }
}
