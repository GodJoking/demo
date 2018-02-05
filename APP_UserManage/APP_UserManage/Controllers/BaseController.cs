using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace APP_UserManage.Controllers
{
    public class BaseController : ApiController
    {
        protected string RequestIP { get; set; }

        //public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        //{
        //    IEnumerable<string> ips;
        //    if(controllerContext.Request.Headers.TryGetValues("X-Forwarded-For",out ips))
        //    {
        //        RequestIP = ips.FirstOrDefault();
        //    }
        //    var a = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    LogHelper.Info("请求信息", "请求IP：" + RequestIP + "\r\n请求url：" + controllerContext.Request.RequestUri);

        //    Task<HttpResponseMessage> task = base.ExecuteAsync(controllerContext, cancellationToken);

        //    LogHelper.Info("响应信息", task.Result.Content.ReadAsStringAsync().Result);
            
        //    return task;
        //}
        public string GetIP()
        {
            //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
            string userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            var a = HttpContext.Current.Request.ServerVariables.Get("Remote_Addr").ToString();
            //否则直接读取REMOTE_ADDR获取客户端IP地址
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            //前两者均失败，则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }
            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return userHostAddress;
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}
