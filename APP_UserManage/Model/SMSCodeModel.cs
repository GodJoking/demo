using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SMSCodeModel
    {
        public int Id { get; set; }
        
        public string MobileNum { get; set; }

        public string CodeValue { get; set; }

        public DateTime CreateTime { get; set; }

        public int UsedCount { get; set; }
    }
}
