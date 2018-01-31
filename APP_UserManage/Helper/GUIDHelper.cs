using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class GUIDHelper
    {
        public static string GenerateGUID()
        {
            return System.Guid.NewGuid().ToString();
        }
    }
}
