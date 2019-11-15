using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Middleware_CoreWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Login() => View();

        [HttpGet]
        [Authorize]
        public IActionResult Register() => View();

        [HttpGet]
        [Authorize]
        public IActionResult ForgotPWD() => View();
    }
}