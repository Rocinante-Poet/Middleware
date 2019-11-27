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
        [HttpGet]
        public Responsemessage Get()
        {
            var data = Get__StatisticsDb.GetErrorGroup();
            if (data.Count() > 0)
            {
                return succeed<ChartError<int>>(data);
            }
            else
            {
                return Error();
            }
        }


    }
}
