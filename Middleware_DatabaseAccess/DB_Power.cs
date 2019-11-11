using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_DatabaseAccess
{
    public class DB_Power
    {
        public IEnumerable<Powerinfo> GetListPowerinfo()
        {
            return CRUD.ExcuteSql((connection) =>
            {
                return connection.GetList<Powerinfo>();
            });
        }

        public IEnumerable<Powerdetails> GetListPowerdetails()
        {
            return CRUD.ExcuteSql((connection) =>
            {
                return connection.GetList<Powerdetails>();
            });
        }
    }
}