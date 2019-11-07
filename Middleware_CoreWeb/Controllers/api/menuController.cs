using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_CoreWeb.Models;
using Middleware_DatabaseAccess;
using Middleware_Model;

namespace Middleware_CoreWeb.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class menuController : ControllerBase
    {
        [HttpGet]
        public JsonData<menu_model> Get([FromQuery]int limit, int offset, string meunName) => new DB_Menu().GetList(limit, offset,meunName);

    }
}