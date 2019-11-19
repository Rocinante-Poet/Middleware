using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Middleware_CoreWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult LoginMain() => View();

        [HttpGet]
        [Authorize]
        public IActionResult Register() => View();

        [HttpGet]
        [Authorize]
        public IActionResult ForgotPWD() => View();

        public async Task<IActionResult> exit()
        {
            await Task.Run(() => { HttpContext.Response.Cookies.Delete("access_token"); });
            return Redirect(CoreConfiguration.DefaultLogin);
        }

        [AllowAnonymous]
        public IActionResult Error(int id)
        {
            var model = ErrorModel.CreateById(id);
            if (id != 403)
            {
                var returnUrl = Request.Query[CookieAuthenticationDefaults.ReturnUrlParameter].ToString();
                if (!string.IsNullOrEmpty(returnUrl)) model.ReturnUrl = returnUrl;
            }
            return View(model);
        }
        
    }
}