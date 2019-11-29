using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_Tool
{
    public class ChartError<T>
    {
        public ChartError()
        {
            data = new List<T>();
        }
        public string name { get; set; }

        public string type { get; set; }

        public List<T> data { get; set; }
    }
}
