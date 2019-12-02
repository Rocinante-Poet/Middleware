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
    public class ResponseStatisticsController : ApiBaseController
    {
        private DB_Task db = new DB_Task();

        [HttpGet("{id}")]
        public Basemessage Get(int id)
        {
            IEnumerable<Chart> data = new List<Chart>();
            switch (id)
            {
                case 1:
                    //data = db.GetStatisticsR();
                    break;

                case 10:
                    data = db.GetTables(id);
                    break;

                case 11:
                    data = db.GetTables(id);
                    break;
            }

            if (data.Any())
            {
                return succeed<Chart>(data);
            }
            else
            {
                return Error();
            }
        }

        [HttpGet("Stack")]
        public ChartDataPile ViveStack([FromQuery]string txt)
        {
            ChartDataPile dt = db.GetChartDataWorkshop(txt);

            return dt;
        }
    }
}