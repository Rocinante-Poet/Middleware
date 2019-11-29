using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Middleware_CoreWeb.Controllers
{
    [Authorize]
    public class TransportController : Controller
    {
        public IActionResult Ndc() => View(new NavigatorBarModel(this));

        public IActionResult Equipment_error() => View(new NavigatorBarModel(this));

        public IActionResult Equipment_statistics() => View(new NavigatorBarModel(this));
    }
}