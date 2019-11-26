using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using Middleware_Tool;
using Microsoft.AspNetCore.Authorization;

namespace Middleware_CoreWeb.Controllers.api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        private DB_User Get_UserpDb = new DB_User();

        [HttpGet]
        public JsonData<Userinfo> Get([FromQuery]int limit, int offset, string Name, int Group) => Get_UserpDb.GetList(limit, offset, Name, Group);

        [HttpPut("add")]
        public Responsemessage Put([FromBody]Userinfo item)
        {
            if (Get_UserpDb.Get(item).Count() > 0)
            {
                return Error("账户已存在");
            }
            else
            {
                item.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                return Get_UserpDb.Add(item) ? succeed() : Error();
            }
        }

        [HttpDelete]
        public Responsemessage delete([FromBody]List<Userinfo> itemArray) => Get_UserpDb.Delete(itemArray) ? succeed() : Error();

        [HttpPut("edit")]
        public Responsemessage Putedit([FromBody]Userinfo item) => Get_UserpDb.Update(item) ? succeed() : Error();
    }
}