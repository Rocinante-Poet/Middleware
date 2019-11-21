using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_CoreWeb;
using System.IO;
using Middleware_Tool;

namespace Middleware_CoreWeb.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogapiController : ApiBaseController
    {
        private DB_Log Get_LogDb = new DB_Log();

        [HttpGet("login")]
        public async Task<JsonData<Operatinginfo>> Get([FromQuery]int limit, int offset, string Name) => await Get_LogDb.GetList(limit, offset, Name);


        [HttpGet("error")]
        public async Task<JsonData<error_model>> Geterror([FromQuery]int limit, int offset, string Name) => await Get_LogDb.GetErrorList(limit, offset, Name);

        [HttpGet("file")]
        public Responsemessage GetlogArray()
        {
            try
            {
                string ErrorFilePath = Directory.GetCurrentDirectory() + @"\log\Error";
                string InfoFilePath = Directory.GetCurrentDirectory() + @"\log\Info";
                DirectoryInfo Errordirectory = new DirectoryInfo(ErrorFilePath);
                DirectoryInfo Infodirectory = new DirectoryInfo(InfoFilePath);
                List<LogFileInfo> logList = new List<LogFileInfo>();
                if (Errordirectory.Exists)
                {
                    foreach (FileInfo file in Errordirectory.GetFiles())
                    {
                        logList.Add(new LogFileInfo() { fileName = file.Name });
                    }
                }
                if (Infodirectory.Exists)
                {
                    foreach (FileInfo file in Infodirectory.GetFiles())
                    {
                        logList.Add(new LogFileInfo() { fileName = file.Name });
                    }
                }
                return succeed<LogFileInfo>(logList);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}