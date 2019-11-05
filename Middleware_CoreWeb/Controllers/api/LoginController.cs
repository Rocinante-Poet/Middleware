using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_Model;
using Newtonsoft.Json;

namespace Middleware_CoreWeb.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private DB_User db = new DB_User();

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns> Json </returns>
        [HttpPost]//("/Login")
        public JsonResult LoginUser([FromBody] Userinfo _user)
        {
            var _return = db.DBLogin(_user);

            if (_return)
                return new JsonResult(new { Success = true, Message = "登录成功" });
            return new JsonResult(new { Success = false, Message = "登录失败" });
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns> Json </returns>
        [HttpPut]//("/Reg")
        public JsonResult Register([FromBody] Userinfo _user)
        {
            var _return = db.DBRegister(_user);

            if (_return)
                return new JsonResult(new { Success = true, Message = "注册成功" });
            return new JsonResult(new { Success = false, Message = "注册失败" });
        }
    }
}