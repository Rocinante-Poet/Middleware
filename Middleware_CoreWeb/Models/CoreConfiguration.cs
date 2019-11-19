using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware_CoreWeb
{
    public class CoreConfiguration
    {
        public static string CookiesUserKey { get { return "access_User"; } }


        public static string JwtCookiesTokenKey { get { return "access_token"; } }


        public static string DefaultLogin { get { return "/Home/LoginMain"; } }

    }
}
