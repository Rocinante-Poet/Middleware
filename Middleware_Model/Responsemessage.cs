﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_Model
{
    public class Responsemessage
    {
        public int state { get; set; }

        public string message { get; set; }

        public List<object> Data { get; set; }
    }
}