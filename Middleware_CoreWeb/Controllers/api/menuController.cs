using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_Model;

namespace Middleware_CoreWeb.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        [HttpGet]
        public JsonData<menu_model> Get([FromQuery]int limit, int offset, string meunName) => new DB_Menu().GetList(limit, offset, meunName);
    }
}