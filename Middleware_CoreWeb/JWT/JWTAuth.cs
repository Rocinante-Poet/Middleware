using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
            if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            #region 【标记】过滤器

            var endpoint = httpContext.GetEndpoint();
            var authorizeData = endpoint?.Metadata.GetOrderedMetadata<IAuthorizeData>() ?? Array.Empty<IAuthorizeData>();
            // 没有 [Authorize] 不需要拦截
            if (authorizeData == null || authorizeData.Count == 0)
            {
                await _next(httpContext);
                return;
            }
            // 有 [AllowAnonymous] 不需要拦截
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(httpContext);
                return;
            }

            #endregion 【标记】过滤器

            //var result = await AuthorizationService(httpContext);
            //if (result == false)
            //{
            //    httpContext.Response.StatusCode = StatusCode;
            //    httpContext.Response.Headers.Add("WWW-Authenticate", new Microsoft.Extensions.Primitives.StringValues(loginfailed));
            //    return;
            //}
            //await _next(context);
            //return;

            #region JWT过滤

            //// 签名校验
            //var result = await httpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
            //if (!result.Succeeded)
            //{
            //    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //    await httpContext.Response.WriteAsync("Unauthorized");
            //    await _next(httpContext);
            //    return;
            //}
            //else
            //{
            //    httpContext.User = result.Principal;
            //}

            var result = await AuthorizationService(httpContext);

            if (result == false)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response.WriteAsync("Unauthorized");
                return;
            }

            await _next(httpContext);
            return;

            #endregion JWT过滤
        }

        private async Task<bool> AuthorizationService(HttpContext context)
        {
            var result = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

            if (!result.Succeeded)
            { return false; }

            string requestUrl = context.Request.Path.Value;

            //var _id = result.Principal.Claims.ToList().FirstOrDefault(x => x.Type == "Uid").Value;
            //var _name = result.Principal.Claims.ToList().FirstOrDefault(x => x.Type == "Uname").Value;
            var _role = result.Principal.Claims.ToList().FirstOrDefault(x => x.Type == "Role").Value;

            var b = _upload.IsHasRole(int.Parse(_role), requestUrl);

            return true;
        }
    }
}