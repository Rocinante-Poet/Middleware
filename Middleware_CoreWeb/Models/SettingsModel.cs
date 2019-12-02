using Microsoft.AspNetCore.Mvc;
using Middleware_Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware_CoreWeb
{
    public class SettingsModel : NavigatorBarModel
    {
        public SettingsModel(ControllerBase controller) : base(controller)
        {

        }

        public List<string> CacheKeyList => CacheFactory.GetCache.GetCacheKeys();

    }
}
