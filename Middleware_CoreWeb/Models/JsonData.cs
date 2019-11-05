using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware_CoreWeb.Models
{
    /// <summary>
    /// bootstrap数据对象
    /// </summary>
    /// <typeparam name="T">数据对象</typeparam>
    public class JsonData<T> where T : class
    {
        public int total { get; set; } //数据总数

        public IEnumerable<T> rows { get; set; } //结果集
    }
}
