using Dapper;
using Middleware_Tool;
using System.Collections.Generic;

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