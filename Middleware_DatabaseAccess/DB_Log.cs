using Dapper;
using Middleware_Tool;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Middleware_DatabaseAccess
{
    public class DB_Log
    {
        public async Task SetOperatingLogAsync(Operatinginfo log)
        {
            var connection = CRUD.GetOpenConnection();
            _ = await connection.InsertAsync(log);
        }
    }
}