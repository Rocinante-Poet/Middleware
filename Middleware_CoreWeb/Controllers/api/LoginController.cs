using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_CoreWeb;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Middleware_Tool;

namespace Middleware_CoreWeb.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ApiBaseController
    {
        private readonly IJWTTokenService _tokenServic;

        public LoginController(IJWTTokenService token)
        {
            _tokenServic = token;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns> Json </returns>
        [HttpPost]
        public async Task<JsonResult> LoginGetToken([FromBody] JWTUserModel _user)
        {
            var _return = await new DB_User().DBLoginAsync(_user);
            var ipaddress = HttpContext.Connection.RemoteIpAddress.ToIPv4String();
            var userAgent = HttpContext.Request.Headers["User-Agent"];
            var agent = new UserAgent(userAgent);
            var Browser = $"{agent.Browser?.Name} {agent.Browser?.Version}";
            var OS = $"{agent.OS?.Name} {agent.OS?.Version}";
            await new DB_Log().SetOperatingLogAsync(new Operatinginfo
            {
                UserID = _user.id,
                Operating = "登录",
                Date = DateTime.Now.ToString(),
                UserName = _user.Name,
                ip = ipaddress,
                Browser = Browser,
                OS = OS,
                state = _return != null ? 200 : 500,
                Details = _return != null ? "通过登录授权" : "未通过登录授权"
            });
            if (_return != null && !string.IsNullOrWhiteSpace(_return.Name) && !string.IsNullOrWhiteSpace(_return.Pwd))
            {
                var token = _tokenServic.GetToken(_user);
                HttpContext.AddCookie(CoreConfiguration.CookiesUserKey, _return.id.ToString().AESEncrypt());
                HttpContext.AddCookie(CoreConfiguration.JwtCookiesTokenKey, token.AESEncrypt());
                return new JsonResult(new { Success = true, Message = "登录成功" });
            }
            return new JsonResult(new { Success = false, Message = "用户名或密码不正确！" });
        }



        /// <summary>
        /// 注册
        /// </summary>
        /// <returns> Json </returns>
        [HttpPut]
        public async Task<JsonResult> RegisterAsync([FromBody] JWTUserModel _user)
        {
            var _return = await new DB_User().DBRegisterAsync(_user);

            if (_return)
                return new JsonResult(new { Success = true, Message = "注册成功" });
            return new JsonResult(new { Success = false, Message = "注册失败" });
        }


    }
}