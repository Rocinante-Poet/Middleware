using IdentityModel;
using Microsoft.AspNetCore.Http;
using Middleware_CoreWeb;
using Middleware_DatabaseAccess;
using Middleware_Tool;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware_CoreWeb
{
    public static class appInfo
    {
        public static Userinfo GetUser(this HttpContext context)
        {
            int userId = context.GetCookie(CoreConfiguration.CookiesUserKey).AESDecrypt().ToInt();

            var userInfo = new DB_User().GetUser(userId);
            if (userId == 0 || userInfo == null)
            {
                context.RemoveCookie(CoreConfiguration.JwtCookiesTokenKey);
                context.RemoveCookie(CoreConfiguration.CookiesUserKey);
                throw new Exception("获取用户信息失败");
            }
            userInfo.group = new DB_Group().Get(userInfo.Power_ID);
            return userInfo;
        }
    }
}