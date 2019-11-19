using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Middleware_CoreWeb.Controllers
{
    [Authorize]
    public class LogViewController : Controller
    {
        public IActionResult LoginLog()
        {

            return View(new NavigatorBarModel(this));
        }

        public IActionResult Error() => View(new NavigatorBarModel(this));


        public IActionResult DownloadLog(string fileName)
        {
            var LogErrorUrl = Directory.GetCurrentDirectory() + $@"\log\Error\{fileName}";
            var LogInfoUrl = Directory.GetCurrentDirectory() + $@"\log\Info\{fileName}";
            var IsErrorExistx = System.IO.File.Exists(LogErrorUrl);
            var IsInfoExistx = System.IO.File.Exists(LogInfoUrl);
            if (!string.IsNullOrEmpty(fileName) && (IsErrorExistx || IsInfoExistx))
            {
                FileStream fs ;
                if (IsErrorExistx)
                    fs = new FileStream(LogErrorUrl, FileMode.Open);
                else
                    fs = new FileStream(LogInfoUrl, FileMode.Open);
                return File(fs, "application/vnd.android.package-archive", fileName);
            }
            else
            {
                return NotFound();
            }
        }
    }
}