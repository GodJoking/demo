using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class RandomHelper
    {
        public static string GetRandom()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
