﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LogInfoModel
    {
        public string UserId { get; set; }

        public DateTime LoginTime { get; set; }

        public string LoginIP { get; set; }

        public string SystemTypeId { get; set; }

        public string EquipmentNum { get; set; }
    }
}
