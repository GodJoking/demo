using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Helper
{
    public class LogHelper
    {
        private static readonly ILog logError = LogManager.GetLogger("logError");
        private static readonly ILog logInfo = LogManager.GetLogger("logInfo");

        public static void Error(Exception ex)
        {
            string output = "时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + "异常信息：" + ex.Message + "\r\n" + "堆栈信息：" + ex.StackTrace + "\r\n\r\n";
            logError.Error(output);
        }

        public static void Info(string title, string msg)
        {
            string output = "时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + "标题：" + title + "\r\n" + "内容：" + msg + "\r\n\r\n";
            logInfo.Info(output);
        }
    }
}
