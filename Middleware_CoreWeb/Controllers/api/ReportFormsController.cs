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
    public class ReportFormsController : ApiBaseController
    {
        private DB_Statistics Get__StatisticsDb = new DB_Statistics();
        //// GET: api/ReportForms
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/ReportForms/5
        [HttpGet("{id}", Name = "Get")]
        public Responsemessage Get(int id)
        {
            IEnumerable<Chart> data = new List<Chart>();
            switch (id)
            {
                case 1:
                    data = Get__StatisticsDb.Get();
                    break;
                case 2:
                    data = Get__StatisticsDb.GetErrorType();
                    break;
            }
            if (data.Count() > 0)
            {
                return succeed<Chart>(data);
            }
            else
            {
                return Error();
            }
        }

        //// POST: api/ReportForms
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/ReportForms/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}





    }
}
