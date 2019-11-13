using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_Tool;
using System.Collections.Generic;

namespace Middleware_CoreWeb.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        private DB_User Get_UserpDb = new DB_User();

        [HttpGet]
        public JsonData<Userinfo> Get([FromQuery]int limit, int offset, string Name, int Group) => Get_UserpDb.GetList(limit, offset, Name, Group);

        [HttpPut("add")]
        public Responsemessage Put([FromBody]Userinfo item) => Get_UserpDb.Add(item) ? succeed() : Error();

        [HttpDelete]
        public Responsemessage delete([FromBody]List<Userinfo> itemArray) => Get_UserpDb.Delete(itemArray) ? succeed() : Error();

        [HttpPut("edit")]
        public Responsemessage Putedit([FromBody]Userinfo item) => Get_UserpDb.Update(item) ? succeed() : Error();
    }
}