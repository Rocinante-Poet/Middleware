using Dapper;
using Middleware_CoreWeb;
using Middleware_Tool;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_DatabaseAccess
{
    public class DB_Power
    {
        public IEnumerable<Powerinfo> GetListPowerinfo()
        {
            var connection = CRUD.GetOpenConnection();
            return connection.GetList<Powerinfo>();
        }

        public IEnumerable<Powerdetails> GetListPowerdetails()
        {
            var connection = CRUD.GetOpenConnection();
            return connection.GetList<Powerdetails>();
        }
    }
}