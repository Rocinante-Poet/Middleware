using Middleware_DatabaseAccess;
using Middleware_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware_CoreWeb
{
    public class NavigatorBarModel : ModelBase
    {
        public IEnumerable<NavigatorItem> NavigatorBarList =>
            new DB_Menu().GetFatherList().Select(
                p => new NavigatorItem()
                {
                    FatherItem = p,
                    sonMenuItem = new DB_Menu().sonItemMenu(p.id)
                });


        public IEnumerable<group_model> groupArray => new DB_Group().Get();
    }
}