using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_CoreWeb;
using System.Collections.Generic;
using System.Linq;
using Middleware_Tool;
using System.Threading.Tasks;

namespace Middleware_CoreWeb
{
    public class NavigatorBarModel : ModelBase
    {
        private ControllerBase controllerBase;

        public NavigatorBarModel(ControllerBase controller)
        {
            controllerBase = controller;
        }

        public async Task<IEnumerable<NavigatorItem>> NavigatorBarList() => new DB_detail().NavigatorBarList((await controllerBase.HttpContext.GetUserAsync()).Power_ID, controllerBase.Request.Path);

        public IEnumerable<NavigatorItem> NavigatorMenuList => new DB_Menu().GetFatherList();

        public IEnumerable<group_model> groupArray => new DB_Group().Get();

        public async Task<Userinfo> GetUserinfo() => await controllerBase.HttpContext.GetUserAsync();

        public string CoreWebTheme
        {
            get
            {
                var _WebTheme = controllerBase.HttpContext.GetCookie("WebTheme");
                if (!string.IsNullOrWhiteSpace(_WebTheme))
                {
                    return _WebTheme;
                }
                else
                {
                    return "lte";
                }
            }
        }
    }
}