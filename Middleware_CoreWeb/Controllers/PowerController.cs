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

        public IActionResult Group() => View(new NavigatorBarModel(this));

        public IActionResult UserManage() => View(new NavigatorBarModel(this));

        public IActionResult IconView() => View();

        public IActionResult Order() => View(new NavigatorBarModel(this));

        public IActionResult QuestionandAnswer()
        {
            MarkDownList md = new MarkDownList(this);
            string _ndc = System.IO.File.ReadAllText($"{Directory.GetCurrentDirectory()}{@"\wwwroot\QA\ndc.md"}", Encoding.UTF8);

            md.Listmd.Add(new MarkDownText()
            {
                MdTitle = "ndc",
                MdContent = new MarkdownSharp.Markdown().Transform(_ndc)
            });

            string _agv = System.IO.File.ReadAllText($"{Directory.GetCurrentDirectory()}{@"\wwwroot\QA\agv.md"}", Encoding.UTF8);
            md.Listmd.Add(new MarkDownText()
            {
                MdTitle = "agv",
                MdContent = new MarkdownSharp.Markdown().Transform(_agv)
            });

            string _a = System.IO.File.ReadAllText($"{Directory.GetCurrentDirectory()}{@"\wwwroot\QA\2019-09-26 子网掩码.md"}", Encoding.UTF8);
            md.Listmd.Add(new MarkDownText()
            {
                MdTitle = "a",
                MdContent = new MarkdownSharp.Markdown().Transform(_a)
            });

            string _b = System.IO.File.ReadAllText($"{Directory.GetCurrentDirectory()}{@"\wwwroot\QA\2018-10-06 被感染的蚂蚁会主动隔离自己.md"}", Encoding.UTF8);
            md.Listmd.Add(new MarkDownText()
            {
                MdTitle = "b",
                MdContent = new MarkdownSharp.Markdown().Transform(_b)
            });

            string _c = System.IO.File.ReadAllText($"{Directory.GetCurrentDirectory()}{@"\wwwroot\QA\2019-01-03 WPF查找子控件.md"}", Encoding.UTF8);
            md.Listmd.Add(new MarkDownText()
            {
                MdTitle = "c",
                MdContent = new MarkdownSharp.Markdown().Transform(_c)
            });
            return View(md);
        }

        public IActionResult Statistics() => View(new NavigatorBarModel(this));


        public IActionResult PersonalCenter() => View(new NavigatorBarModel(this));


        public IActionResult Settings() => View(new NavigatorBarModel(this));

    }
}