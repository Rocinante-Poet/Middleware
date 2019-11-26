using Dapper;
using Middleware_CoreWeb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Middleware_Tool;

namespace Middleware_DatabaseAccess
{
    public class DB_Log
    {
        public async Task SetOperatingLogAsync(Operatinginfo log)
        {
            var connection = CRUD.GetOpenConnection();
            _ = await connection.InsertAsync(log);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Pagelimit"></param>
        /// <param name="Pageoffset"></param>
        /// <param name="meunName"></param>
        /// <returns></returns>
        public async Task<JsonData<Operatinginfo>> GetList(int Pagelimit, int Pageoffset, string Name)
        {
            var connection = CRUD.GetOpenConnection();

            StringBuilder strQuery = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Name))
            {
                strQuery.Append("where UserName like @UserName");
            }
            var List = await connection.GetListPagedAsync<Operatinginfo>((Pageoffset / Pagelimit) + 1, Pagelimit, strQuery.ToString(), "ID desc ", new { UserName = $"%{Name}%" });
            var CountPage = await connection.RecordCountAsync<Operatinginfo>(strQuery.ToString(), new { UserName = $"%{Name}%" });
            return new JsonData<Operatinginfo>() { rows = List, total = CountPage };
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Pagelimit"></param>
        /// <param name="Pageoffset"></param>
        /// <param name="meunName"></param>
        /// <returns></returns>
        public async Task<JsonData<error_model>> GetErrorList(int Pagelimit, int Pageoffset, string Name)
        {
            var connection = CRUD.GetOpenConnection();

            StringBuilder strQuery = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Name))
            {
                strQuery.Append("where User like @User");
            }
            var List = await connection.GetListPagedAsync<error_model>((Pageoffset / Pagelimit) + 1, Pagelimit, strQuery.ToString(), "ID desc ", new { User = $"%{Name}%" });
            var CountPage = await connection.RecordCountAsync<error_model>(strQuery.ToString(), new { User = $"%{Name}%" });
            return new JsonData<error_model>() { rows = List, total = CountPage };
        }


        public async Task<int?> InsertError(Userinfo userinfo, Exception exception, HttpContext context)
        {
            var connection = CRUD.GetOpenConnection();
            return await connection.InsertAsync(new error_model()
            {
                time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                url = context.Request.Path.Value,
                errormeg = exception.Message,
                User = userinfo.Name
            });
        }
    }
}