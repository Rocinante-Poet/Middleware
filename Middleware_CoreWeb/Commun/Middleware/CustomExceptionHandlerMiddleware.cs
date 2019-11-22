using Microsoft.AspNetCore.Http;
using Middleware_CoreWeb;
using Middleware_DatabaseAccess;
using Middleware_Tool;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Middleware_CoreWeb
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log4net.WriteError(ex.Message, ex);
                await new DB_Log().InsertError(await context.GetUserAsync(), ex, context);
                bool IsAjaxCall = context.Request.Headers["x-requested-with"] == "XMLHttpRequest";
                if (IsAjaxCall || context.Request.Path.StartsWithSegments("/api"))
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(new Responsemessage() { state = 500, message = "系统出错啦，相关异常信息已记录到日志，请联系管理员处理！", }.ToJson());
                }
                else
                {
                    context.Response.Headers["Expires"] = " Mon, 26 Jul 1997 05:00:00 GMT";
                    context.Response.Headers["Pragma"] = "no-cache";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.Redirect("/Home/Error/500");
                }
            }
        }
    }
}