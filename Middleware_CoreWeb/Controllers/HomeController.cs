using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpGet]
        public IActionResult Register() => View();

        [HttpGet]
        [Authorize]
        public IActionResult ForgotPWD() => View();
    }
}