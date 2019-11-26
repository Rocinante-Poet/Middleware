using Middleware_DatabaseAccess;
using Middleware_CoreWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Middleware_Tool;

namespace Middleware_CoreWeb
{
    public class JWTUpload
    {
        public static List<Powerdetails> powerdetails = new List<Powerdetails>();


        public async Task UpdateRole()
        {
            await Task.Run(() =>
            {
                powerdetails = new DB_Power().GetListPowerdetails().ToList();
            });
        }

        public bool IsHasRole(int role, string url)
        {
            Powerdetails _p = powerdetails.FirstOrDefault(x => x.FunctionUrl != null && x.FunctionUrl.Trim().Replace("/", "").ToLower() == url.Trim().Replace("/", "").ToLower());
            if (_p != null)
                return _p.PowerGroupID >= role;
            return false;
        }
    }
}