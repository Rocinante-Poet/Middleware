using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Middleware_CoreWeb
{
    public class JWTAuthentication
    {
        private readonly RequestDelegate _next;

        public JWTAuthentication(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var result = await httpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

            if (!result.Succeeded)
            {
                httpContext.User = result.Principal;
                await _next.Invoke(httpContext);
                //httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //await httpContext.Response.WriteAsync("Unauthorized");
            }
            else
            {
                httpContext.User = result.Principal;
                await _next.Invoke(httpContext);
            }
        }
    }
}