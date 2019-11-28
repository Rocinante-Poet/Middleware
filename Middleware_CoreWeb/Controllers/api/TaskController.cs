using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_Tool;

namespace Middleware_CoreWeb.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ApiBaseController
    {
        #region NDC

        private DB_Task db = new DB_Task();

        [HttpGet]
        public JsonData<Transportndc_model> Get([FromQuery]int limit, int offset) => db.GetList(limit, offset);

        [HttpPut("add")]
        public Basemessage Put([FromForm]Transportndc_model item)
        {
            return db.Add(item) ? succeed() : Error();
        }

        [HttpDelete]
        public Basemessage delete([FromBody]List<Transportndc_model> itemArray) => db.Delete(itemArray) ? succeed() : Error();

        [HttpPut("edit")]
        public Basemessage Putedit([FromForm]Transportndc_model item) => db.Update(item) ? succeed() : Error();

        #endregion NDC
    }
}