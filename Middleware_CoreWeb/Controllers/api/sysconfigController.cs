using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_Tool;


namespace Middleware_CoreWeb.Controllers.api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class sysconfigController : ApiBaseController
    {
        [HttpGet]
        public Basemessage Get([FromQuery]string themeName)
        {
            if (!string.IsNullOrWhiteSpace(themeName))
            {
                HttpContext.AddCookie("WebTheme", themeName);
                return succeed();
            }
            else
            {
                return Error();
            }
        }
        [HttpPost]
        public Basemessage ClearCache()
        {
            CacheFactory.GetCache.RemoveAll();
            return succeed();
        }
    }
}
