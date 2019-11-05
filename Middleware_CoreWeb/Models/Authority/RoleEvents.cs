using CZGL.Auth.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware_CoreWeb.Models
{
    public class RoleEvents : IRoleEventsHadner
    {
        /// <summary>
        /// 授权开始前
        /// </summary>
        public async Task Start(HttpContext httpContext)
        {
            await Task.CompletedTask;
        }

        public void TokenEbnormal(object eventsInfo)
        {
            throw new NotImplementedException();
        }

        public void TokenIssued(object eventsInfo)
        {
            throw new NotImplementedException();
        }

        public void NoPermissions(object eventsInfo)
        {
            throw new NotImplementedException();
        }

        public void Success(object eventsInfo)
        {
            throw new NotImplementedException();
        }

        public async Task End(HttpContext httpContext)
        {
            await Task.CompletedTask;
        }
    }
}