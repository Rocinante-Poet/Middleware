using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware_Tool
{
    public static class CookieManage
    {
        /// <summary>
        /// 添加cookies
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minutes"></param>
        public static void AddCookie(this HttpContext context, string key, string value, int minutes = 60 * 24 * 7)
        {
            context.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            });
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        public static string GetCookie(this HttpContext context, string key)
        {
            context.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }

        /// <summary>
        /// 删除cookies
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        public static void RemoveCookie(this HttpContext context, string key)
        {
            context.Response.Cookies.Delete(key);
        }
    }
}