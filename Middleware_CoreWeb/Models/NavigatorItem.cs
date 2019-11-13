using Middleware_Tool;
using System.Collections.Generic;

namespace Middleware_CoreWeb
{
    public class NavigatorItem
    {
        public menu_model FatherItem { get; set; }

        public IEnumerable<menu_model> sonMenuItem { get; set; }
    }
}