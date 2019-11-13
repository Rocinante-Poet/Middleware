using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middleware_DatabaseAccess;
using Middleware_Model;

namespace Middleware_CoreWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public  IActionResult Login()
        {
            return View();
        }

        public IActionResult Register() => View();

        public IActionResult ForgotPWD() => View();
    }
}