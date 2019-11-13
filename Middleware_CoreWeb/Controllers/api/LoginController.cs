using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Newtonsoft.Json;

namespace Middleware_CoreWeb.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
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

            if (_return)
            {
                var token = _tokenServic.GetToken(_user);
                var response = new
                {
                    token_type = JwtBearerDefaults.AuthenticationScheme,
                    access_token = token,
                    profile = new
                    {
                        sid = _user.UserID,
                        name = _user.Name,
                    }
                };

                var GetToken = JsonConvert.SerializeObject(response);

                return new JsonResult(new { Success = true, Message = "登录成功" });
            }
            return new JsonResult(new { Success = false, Message = "登录失败" });
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