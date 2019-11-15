using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_Tool;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

            if (_return.Name != null)
            {
                var token = _tokenServic.GetToken(_user);

                SetCookies("access_token", AESEncrypt(token));

                await new DB_Log().SetOperatingLogAsync(new Operatinginfo
                {
                    UserID = _user.UserID,
                    Operating = "登录",
                    Date = DateTime.Now.ToString(),
                    Details = "通过登录授权"
                });

                return new JsonResult(new { Success = true, Message = "登录成功", jwttoken = token });
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

        protected void SetCookies(string key, string value, int minutes = 60 * 24 * 7)
        {
            HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            });
        }

        protected void DeleteCookies(string key)
        {
            HttpContext.Response.Cookies.Delete(key);
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        protected string GetCookies(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }

        public static string AESEncrypt(string value, string _aeskey = "[23|*kjenHU~'e;]")
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(_aeskey);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(value);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }
}