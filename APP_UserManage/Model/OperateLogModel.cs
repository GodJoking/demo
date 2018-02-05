using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OperateLogModel
    {
        public string UserId { get; set; }

        public int OperateTypeId { get; set; }

        public DateTime OperateTime { get; set; }

        public string OperateIP { get; set; }

        public int SystemTypeId { get; set; }

        public string EquipmentNum { get; set; }
    }
}
