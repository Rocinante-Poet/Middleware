using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Middleware_CoreWeb.Controllers
{
    [Authorize]
    public class PowerController : Controller
    {
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Main() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        public IActionResult Menu() => View(new NavigatorBarModel(this));


        public IActionResult Group()
        {
            return View(new NavigatorBarModel(this));

        }

        public IActionResult UserManage() => View(new NavigatorBarModel(this));


        public IActionResult IconView() => View();
    }
}