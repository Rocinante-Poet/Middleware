using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public IActionResult QuestionandAnswer()
        {
            string p = @"\wwwroot\QA\2019-04-12 MySQL和Redis.md";
            var lll = Directory.GetCurrentDirectory() + p;
            string markdowntext = System.IO.File.ReadAllText($"{Directory.GetCurrentDirectory()}{p}", Encoding.UTF8);

            MarkdownSharp.Markdown m = new MarkdownSharp.Markdown();
            string html = m.Transform(markdowntext);
            MarkDownText md = new MarkDownText()
            {
                title = "AGV",
                content = html
            };
            return View(new NavigatorBarModel(this));
        }

        public IActionResult Order() => View(new NavigatorBarModel(this));


    }
}