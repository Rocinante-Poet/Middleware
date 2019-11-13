using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_Model;

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
        public Responsemessage Put([FromBody]menu_model menu) => Get_MenuDb.AddMenu(menu) ? succeed() : Error();

        [HttpDelete]
        public Responsemessage delete([FromBody]List<menu_model> menuArray) => Get_MenuDb.DeleteMenu(menuArray) ? succeed() : Error();

        [HttpPut("edit")]
        public Responsemessage Putedit([FromBody]menu_model menu) => Get_MenuDb.UpdateMenu(menu) ? succeed() : Error();
    }
}