using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class ConfigHelper
    {
        // 连接字符串
        public static string ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;


        public static string Domain_Admin = GetConfig("Domain_Admin");
        // 短信sn
        public static string SN = GetConfig("sn");
        // 短信验证码有效时间
        public static int SMSTime = ConfigHelper.GetConfig("SMSTime").ToInt();
        public static string SMSMaxCountEveryDay_9H = ConfigHelper.GetConfig("SMSMaxCountEveryDay_9H");
        public static string SMSMaxCountEveryDay_App = ConfigHelper.GetConfig("SMSMaxCountEveryDay_App");

        // 回推
        public static string CallBackUrl_GuoAn = ConfigHelper.GetConfig("CallBackUrl_GuoAn");
        public static string CallBackUrl_ZhongChao = ConfigHelper.GetConfig("CallBackUrl_ZhongChao");

        public static string AppId = ConfigHelper.GetConfig("AppId");
        // 盐
        public static string Salt = ConfigHelper.GetConfig("Salt");

        public static string GetConfig(string key)
        {
            string value = "";
            try
            {
                value = ConfigurationManager.AppSettings[key].Trim();
            }
            catch
            {
                value = "";
            }
            return string.IsNullOrEmpty(value) ? "" : value;
        }
    }
}
