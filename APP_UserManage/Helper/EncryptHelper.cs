using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class EncryptHelper
    {
        public static string MD5Encrypt(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes_New = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(bytes_New).Replace("-", "");
        }

        public static string MD5Encrypt_ZC(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes_New = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(bytes_New).Replace("-", "").TrimStart('0');
        }
    }
}
