using Middleware_DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware_CoreWeb
{
    public class JWTUpload
    {
        public static List<Powerdetails> powerdetails = new List<Powerdetails>();

        //public static Dictionary<string, List<Powerdetails>> powerdetails = new Dictionary<string, List<Powerdetails>>();

        //public static List<Powerinfo> powerinfo = new List<Powerinfo>();

        public async Task UpdateRole()
        {
            await Task.Run(() =>
            {
                powerdetails = new DB_Power().GetListPowerdetails().ToList();
                //powerinfo = new DB_Power().GetListPowerinfo().ToList();
            });
        }

        // 检查是否存在此角色
        public bool IsHasRole(int role, string url)
        {
            Powerdetails _p = powerdetails.Find(x => x.FunctionUrl.Trim().Replace("/", "").ToLower() == url.Trim().Replace("/", "").ToLower());
            if (_p != null)
                return _p.PowerGroupID >= role;
            return false;
        }
    }
}