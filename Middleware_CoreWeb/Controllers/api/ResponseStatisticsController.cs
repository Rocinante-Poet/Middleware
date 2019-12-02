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

        [HttpGet("Stack")]
        public ChartDataPile ViveStack([FromQuery]string txt, string line, string sum)
        {
            ChartDataPile dt = db.GetChartDataWorkshop(txt, line, sum);

            return dt;
        }
    }
}