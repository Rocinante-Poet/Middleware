using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Middleware_CoreWeb.Controllers
{
    public class PowerController : Controller
    {
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Main() => View(new NavigatorBarModel());

        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        public IActionResult Menu() => View(new NavigatorBarModel());

        /// <summary>
        /// 管理员
        /// </summary>
        /// <returns></returns>
        public IActionResult Admin() => View();



    }
}