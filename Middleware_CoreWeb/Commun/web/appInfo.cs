﻿using Microsoft.AspNetCore.Http;
using Middleware_DatabaseAccess;
using System.Linq;
using System.Threading.Tasks;
using Middleware_Tool;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityModel;

namespace Middleware_CoreWeb
{
    public static class appInfo
    {
        /// <summary>
        /// 从Token获取用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<Userinfo> GetUserAsync(this HttpContext context)
        {
            AuthenticateResult result = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
            if (result.Principal == null)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(new Basemessage() { state = 500, message = "Token签名错误,请检查是否授权" }.ToJson());
                return new Userinfo();
            }
            var ClaimResult = result.Principal.Claims.ToList().FirstOrDefault(x => x.Type == JwtClaimTypes.Id);
            var userId = ClaimResult != null ? ClaimResult.Value.ToInt() : 0;
            var userInfo = new DB_User().GetUser(userId);
            if (userId == 0 || userInfo == null)
            {
                context.RemoveCookie(CoreConfiguration.JwtCookiesTokenKey);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(new Basemessage() { state = 500, message = "Token签名错误，未找到该用户信息" }.ToJson());
                return new Userinfo();
            }
            userInfo.group = new DB_Group().Get(userInfo.Power_ID);
            return userInfo;
        }
    }
}