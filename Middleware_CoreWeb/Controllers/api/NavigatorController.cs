using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_CoreWeb;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Middleware_Tool;

namespace Middleware_CoreWeb.Controllers.api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NavigatorController : ApiBaseController
    {
        private DB_Menu Get_MenuDb = new DB_Menu();
        private DB_detail Get_detailDb = new DB_detail();

        [HttpGet]
        public async Task<JsonData<menu_model>> Get([FromQuery]int limit, int offset, string meunName) => await Get_MenuDb.GetList(limit, offset, meunName);

        [HttpGet("Getparentmenus")]
        public IEnumerable<menu_model> Getparentmenus() => Get_MenuDb.GetFatherLists();

        [HttpPut("add")]
        public Basemessage Put([FromBody]menu_model menu) => Get_MenuDb.AddMenu(menu) ? succeed() : Error();

        [HttpDelete]
        public Basemessage delete([FromBody]List<menu_model> menuArray) => Get_MenuDb.DeleteMenu(menuArray) ? succeed() : Error();

        [HttpPut("edit")]
        public Basemessage Putedit([FromBody]menu_model menu) => Get_MenuDb.UpdateMenu(menu) ? succeed() : Error();

        [HttpGet("detail")]
        public Basemessage detail([FromQuery] group_model group) => succeed<detail_model>(Get_detailDb.Get(group.id));

        [HttpPost("add")]
        public Basemessage AddDetail([FromBody] IEnumerable<menu_model> menu, [FromQuery] int id) => Get_detailDb.Add(menu, id) ? succeed() : Error();
    }
}