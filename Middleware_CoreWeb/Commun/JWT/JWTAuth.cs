using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Middleware_CoreWeb;
using Middleware_DatabaseAccess;
using Middleware_Tool;
using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Middleware_CoreWeb
{
    public class JWTAuth
    {
        private readonly RequestDelegate _next;

        private JWTUpload _upload = new JWTUpload();

        public JWTAuth(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var endpoint = httpContext.GetEndpoint();
            var authorizeData = endpoint?.Metadata.GetOrderedMetadata<IAuthorizeData>() ?? Array.Empty<IAuthorizeData>();

            if (authorizeData == null || authorizeData.Count == 0)
            {
                await _next(httpContext);
                return;
            }
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(httpContext);
                return;
            }
            string requestUrl = httpContext.Request.Path.Value;
            var result = await AuthorizationService(httpContext);
            bool IsAjaxCall = httpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
            bool IsapiRequest = httpContext.Request.Path.StartsWithSegments("/api");
            if (result.Succeeded == false)
            {
                if (IsAjaxCall || IsapiRequest)
                {
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsync(new Responsemessage() { state = 500, message = "登录过期，无权限访问！" }.ToJson());
                }
                else
                {
                    httpContext.Response.Headers["Expires"] = " Mon, 26 Jul 1997 05:00:00 GMT";
                    httpContext.Response.Headers["Pragma"] = "no-cache";
                    httpContext.Response.Headers["Cache-Control"] = "no-cache, must-revalidate, no-store, no-cache, must-revalidate, post-check=0, pre-check=0";
                    httpContext.Response.Redirect(CoreConfiguration.DefaultLogin);
                }
                return;
            }
            if (!IsAjaxCall && !IsapiRequest)
            {
                var detailList = new DB_detail().NavigatorBarList((await httpContext.GetUserAsync()).Power_ID);

                bool IsPower = false;
                if (detailList != null && !requestUrl.Contains("Download"))
                {
                    foreach (var menuitem in detailList)
                    {
                        if (menuitem.FatherItem.url == requestUrl)
                        {
                            IsPower = true;
                        }
                        var item = menuitem.sonMenuItem.FirstOrDefault(p => p.url == requestUrl);
                        if (item != null)
                        {
                            IsPower = true;
                        }
                    }
                    if (!IsPower)
                    {
                        httpContext.Response.StatusCode = 403; //无权限
                        return;
                    }
                }
            }
            await _next(httpContext);
            return;
        }

        public static async Task<AuthenticateResult> AuthorizationService(HttpContext context)
        {
            string jwt = context.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(jwt))
            {
                context.Request.Cookies.TryGetValue(CoreConfiguration.JwtCookiesTokenKey, out string cookies_token);
                context.Request.Headers.Add("Authorization", $"Bearer {cookies_token.AESDecrypt()}");
            }
            var result = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

            return result;
        }
    }
}