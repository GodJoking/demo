using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class DataFormatHelper
    {
        public static int ToInt(this object val, int defVal = default(int))
        {
            return val.To<Int32>(defVal);
        }

        /// <summary>
        /// 通用类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static T To<T>(this object val, T defVal = default(T))
        {
            if (val == null)
                return (T)defVal;
            if (val is T)
                return (T)val;

            Type type = typeof(T);
            try
            {
                if (type.BaseType == typeof(Enum))
                {
                    return (T)Enum.Parse(type, val.ToString(), true);
                }
                else
                {
                    return (T)Convert.ChangeType(val, type);
                }
            }
            catch
            {
                return defVal;
            }
        }
    }
}
