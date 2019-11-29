using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_Tool
{
    public class Responsemessage
    {
        public int state { get; set; }

        public string message { get; set; }

        public IEnumerable<object> Data { get; set; }
    }

   
}