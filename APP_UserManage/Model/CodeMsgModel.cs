﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public partial class CodeMsgModel
    {
        public int Code { get; set; }

        public string Msg { get; set; }
    }
}
