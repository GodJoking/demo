﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BaseModel
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public object Data { get; set; }

        public string Desc { get; set; }
    }
}
