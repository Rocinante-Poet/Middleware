using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_CoreWeb.Models;
using Middleware_DatabaseAccess;

namespace Middleware_CoreWeb.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class menuController : ControllerBase
    {
        [HttpGet]
        public JsonData<menu_model> Get([FromQuery]int limit, int offset)
        {
            return new JsonData<menu_model>() { rows = new DB_Menu().GetList(), total = 500 };
        }
    }
}