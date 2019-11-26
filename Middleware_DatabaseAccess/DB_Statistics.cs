using Dapper;
using Middleware_Tool;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_DatabaseAccess
{
    public class DB_Statistics
    {
        public IEnumerable<Chart> Get()
        {
           return CRUD.ExcuteSql(connection =>
           {
               StringBuilder sql = new StringBuilder();
               sql.Append(@"select b.name,a.value from (SELECT  equipmentId ,count(*) as value FROM equipment_error where date>=(select DATE_ADD(curdate(),interval -day(curdate())+1 day)) and  date<=
                (select last_day(curdate()))
                GROUP BY equipmentId) as a, equipment as b where a.equipmentId = b.id");
               var data = connection.Query<Chart>(sql.ToString());
               return data;
           });
        }

        public IEnumerable<Chart> GetErrorType()
        {
            return CRUD.ExcuteSql(connection =>
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(@"select a.errorcount as value,b.`errorInfo` as name 
                 from (SELECT  errorType ,count(*) as errorcount FROM equipment_error where date>=(select DATE_ADD(curdate(),interval -day(curdate())+1 day)) and  date<=(select last_day(curdate()))GROUP BY errorType) as a,error_type as b where a.errorType=b.id");
                var data = connection.Query<Chart>(sql.ToString());
                return data;
            });
        }
    }
}
