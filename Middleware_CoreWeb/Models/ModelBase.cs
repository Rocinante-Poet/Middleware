using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware_CoreWeb
{
    public class ModelBase
    {
        /// <summary>
        /// 获取 网站 logo 小图标
        /// </summary>
        public string WebSiteIcon { get; protected set; } = "~/favicon.ico";
    }

    public class MarkDownList : NavigatorBarModel
    {
        public MarkDownList(ControllerBase controller) : base(controller)
        {
        }

        public List<MarkDownText> Listmd = new List<MarkDownText>();
    }

    public class MarkDownText
    {
        public string MdTitle { get; set; }
        public string MdContent { get; set; }
    }
}