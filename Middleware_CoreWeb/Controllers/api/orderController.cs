using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;

namespace Middleware_CoreWeb.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class orderController : ApiBaseController
    {
        private DB_Order Get_OrderDb = new DB_Order();

        [HttpGet]
        public async Task<JsonData<order_model>> Get([FromQuery]int limit, int offset, string boxId) => await Get_OrderDb.GetList(limit, offset, boxId);
    }
}