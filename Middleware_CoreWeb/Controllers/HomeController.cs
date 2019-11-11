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
        private IHttpContextAccessor _accessor;

        public HomeController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        [HttpGet]
        public IActionResult Index(int? id) => View(_accessor.HttpContext);

        [AllowAnonymous]
        public IActionResult Login() => View();

        [Authorize]
        public IActionResult Register() => View();

        [AllowAnonymous]
        public IActionResult ForgotPWD() => View();
    }
}