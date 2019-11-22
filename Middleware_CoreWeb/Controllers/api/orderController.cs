using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_Tool;

namespace Middleware_CoreWeb.Controllers.api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class orderController : ApiBaseController
    {
        private DB_Order Get_OrderDb = new DB_Order();

        [HttpGet]
        public async Task<JsonData<order_model>> Get([FromQuery]int limit, int offset, string boxId) => await Get_OrderDb.GetList(limit, offset, boxId);


        [HttpPut("add")]
        public Responsemessage Put([FromForm]order_model item) => Get_OrderDb.Add(item) ? succeed() : Error();

        [HttpDelete]
        public Responsemessage delete([FromBody]List<order_model> itemArray) => Get_OrderDb.Delete(itemArray) ? succeed() : Error();

        [HttpPut("edit")]
        public Responsemessage Putedit([FromForm]order_model item) => Get_OrderDb.Update(item) ? succeed() : Error();

    }
}