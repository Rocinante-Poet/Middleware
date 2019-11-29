using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_Tool
{

    public class Basemessage
    {
        public int state { get; set; }

        public string message { get; set; }
    }
    public class Responsemessage<T>: Basemessage
    {
        public IEnumerable<T> Data { get; set; }
    }

    public class Jsonmessage<T> : Basemessage
    {
        public T Data { get; set; }
    }


}