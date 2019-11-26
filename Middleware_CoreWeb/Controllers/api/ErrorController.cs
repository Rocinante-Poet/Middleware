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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ErrorController : ApiBaseController
    {
        private DB_Error db = new DB_Error();

        [HttpGet]
        public JsonData<object> Get([FromQuery]int limit, int offset, string sbType, string sbName, int? errorCode, int? errorType) => db.GetList(limit, offset, sbType, sbName, errorCode, errorType);

        [HttpDelete]
        public Responsemessage delete([FromBody]List<Equipment_error> itemArray) => db.Delete(itemArray) ? succeed() : Error();
    }
}