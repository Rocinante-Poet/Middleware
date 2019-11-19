using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_CoreWeb;
using System.Collections.Generic;
using System.Linq;
using Middleware_Tool;

namespace Middleware_CoreWeb
{
    public class NavigatorBarModel : ModelBase
    {
        public NavigatorBarModel(ControllerBase controller)
        {
            GetUserinfo = controller.HttpContext.GetUser();
            NavigatorMenuList = new DB_Menu().GetFatherList();
            NavigatorBarList = new DB_detail().NavigatorBarList(appInfo.GetUser(controller.HttpContext).Power_ID);
        }

        public IEnumerable<NavigatorItem> NavigatorBarList { get; protected set; }


        public IEnumerable<NavigatorItem> NavigatorMenuList { get; protected set; }


        public IEnumerable<group_model> groupArray => new DB_Group().Get();


        public Userinfo GetUserinfo { get; protected set; }
    }
}