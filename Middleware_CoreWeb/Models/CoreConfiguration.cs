using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Middleware_Tool;

namespace Middleware_CoreWeb
{
    public  static class CoreConfiguration
    {
        public static string CookiesUserKey { get { return "access_User"; } }


        public static string JwtCookiesTokenKey { get { return "access_token"; } }


        public static string DefaultLogin { get { return "/Home/Login/"; } }


        public static string DefaultMain { get { return "/Power/Main/"; } }

    }
}
