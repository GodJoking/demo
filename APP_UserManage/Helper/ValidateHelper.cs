using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helper
{
    public class ValidateHelper
    {
        public static bool IsNullOrEmpty(string str)
        {
            return string.IsNullOrEmpty(str) || str == "null" || str == "(null)";
        }

        public static bool Guid(string token)
        {
            Regex regex = new Regex("^[A-Za-z0-9]{8}(-[A-Za-z0-9]{4}){3}-[A-Za-z0-9]{12}$");
            return regex.IsMatch(token);
        }

        public static bool MobileNum(string mobileNum)
        {
            Regex regex = new Regex("^(0|86|17951)?(1)[0-9]{10}$");
            return regex.IsMatch(mobileNum);
        }

        public static bool SMSCode(string smsCode)
        {
            Regex regex = new Regex("[0-9]{6}");
            return regex.IsMatch(smsCode);
        }

        public static bool AuthorizedTypeId(int i)
        {
            return Rang(i, 1, 3); // 放在配置文件中
        }

        public static bool OpenId(string openId)
        {
            return StringLength(openId, 28, 32);
        }

        public static bool AccessToken(string accessToken)
        {
            return StringLength(accessToken, 0, 32);
        }

        public static bool RefreshToken(string refreshToken)
        {
            return StringLength(refreshToken, 0, 32);
        }

        public static bool IP(string ip)
        {
            Regex regex_ipv4 = new Regex(@"^(25[0-5]|2[0-4]\d|[01]?\d\d?)\.(25[0-5]|2[0-4]\d|[01]?\d\d?)\.(25[0-5]|2[0-4]\d|[01]?\d\d?)\.(25[0-5]|2[0-4]\d|[01]?\d\d?)$", RegexOptions.IgnoreCase);

            Regex regex_ipv6 = new Regex(@"^((([0-9A-Fa-f]{1,4}:){7}[0-9A-Fa-f]{1,4})|(([0-9A-Fa-f]{1,4}:){6}:[0-9A-Fa-f]{1,4})|(([0-9A-Fa-f]{1,4}:){5}:([0-9A-Fa-f]{1,4}:)?[0-9A-Fa-f]{1,4})|(([0-9A-Fa-f]{1,4}:){4}:([0-9A-Fa-f]{1,4}:){0,2}[0-9A-Fa-f]{1,4})|(([0-9A-Fa-f]{1,4}:){3}:([0-9A-Fa-f]{1,4}:){0,3}[0-9A-Fa-f]{1,4})|(([0-9A-Fa-f]{1,4}:){2}:([0-9A-Fa-f]{1,4}:){0,4}[0-9A-Fa-f]{1,4})|(([0-9A-Fa-f]{1,4}:){6}((\b((25[0-5])|(1\d{2})|(2[0-4]\d)|(\d{1,2}))\b)\.){3}(\b((25[0-5])|(1\d{2})|(2[0-4]\d)|(\d{1,2}))\b))|(([0-9A-Fa-f]{1,4}:){0,5}:((\b((25[0-5])|(1\d{2})|(2[0-4]\d)|(\d{1,2}))\b)\.){3}(\b((25[0-5])|(1\d{2})|(2[0-4]\d)|(\d{1,2}))\b))|(::([0-9A-Fa-f]{1,4}:){0,5}((\b((25[0-5])|(1\d{2})|(2[0-4]\d)|(\d{1,2}))\b)\.){3}(\b((25[0-5])|(1\d{2})|(2[0-4]\d)|(\d{1,2}))\b))|([0-9A-Fa-f]{1,4}::([0-9A-Fa-f]{1,4}:){0,5}[0-9A-Fa-f]{1,4})|(::([0-9A-Fa-f]{1,4}:){0,6}[0-9A-Fa-f]{1,4})|(([0-9A-Fa-f]{1,4}:){1,7}:))$", RegexOptions.IgnoreCase);

            return regex_ipv4.IsMatch(ip) || regex_ipv6.IsMatch(ip);
        }

        public static bool SystemTypeId(int i)
        {
            return Rang(i, 1, 4); // 放在配置文件中
        }

        public static bool EquipmentNum(string equipmentNum)
        {
            return StringLength(equipmentNum, 128);
        }

        public static bool Name(string name)
        {
            return StringLength(name, 32);
        }

        public static bool Sex(int i)
        {
            return Rang(i, 1, 3);// 放在配置文件中
        }

        public static bool IDNum(string idNum)
        {
            Regex regex = new Regex("^\\d{17}[\\d|x]|\\d{15}$", RegexOptions.IgnoreCase);
            return regex.IsMatch(idNum);
        }

        public static bool StringLength(string str, int maxLength)
        {
            return StringLength(str, 0, maxLength);
        }

        public static bool StringLength(string str, int minLength, int maxLength)
        {
            if (str.Length >= minLength && str.Length <= maxLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Rang(int i, int maxValue)
        {
            return Rang(i, 0, maxValue);
        }

        public static bool Rang(int i, int minValue, int maxValue)
        {
            if (i >= minValue && i <= maxValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
