using Middleware_CoreWeb;
using System.Collections.Generic;

namespace Middleware_Tool
{
    public class NavigatorItem
    {
        public NavigatorItem()
        {
            sonMenuItem = new List<menu_model>();
        }

        public menu_model FatherItem { get; set; }

        public IEnumerable<menu_model> sonMenuItem { get; set; }
    }
}