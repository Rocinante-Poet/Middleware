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
}
