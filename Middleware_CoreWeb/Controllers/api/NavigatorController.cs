using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Middleware_CoreWeb.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigatorController : ApiBaseController
    {
        private DB_Menu Get_MenuDb = new DB_Menu();

        [HttpGet]
        public async Task<JsonData<menu_model>> Get([FromQuery]int limit, int offset, string meunName) => await Get_MenuDb.GetList(limit, offset, meunName);

        [HttpGet("Getparentmenus")]
        public IEnumerable<menu_model> Getparentmenus() => Get_MenuDb.GetFatherList();

        [HttpPut("add")]
        public async Task<Responsemessage> Put([FromBody]menu_model menu) => await Get_MenuDb.AddMenu(menu) ? succeed() : Error();

        [HttpDelete]
        public async Task<Responsemessage> delete([FromBody]List<menu_model> menuArray) => await Get_MenuDb.DeleteMenu(menuArray) ? succeed() : Error();

        [HttpPut("edit")]
        public async Task<Responsemessage> Putedit([FromBody]menu_model menu) => await Get_MenuDb.UpdateMenu(menu) ? succeed() : Error();
    }
}