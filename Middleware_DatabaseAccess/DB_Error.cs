using Dapper;
using Middleware_Tool;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Middleware_DatabaseAccess
{
    public class DB_Error
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        public JsonData<object> GetList(int Pagelimit, int Pageoffset, string sbType, string sbName, int? errorCode, int? errorLevel)
        {
            var connection = CRUD.GetOpenConnection();

            StringBuilder strQuery = new StringBuilder();
            StringBuilder strCount = new StringBuilder();

            strQuery.Append("SELECT a.id,a.equipmentId,a.errorType,a.errorTime,b.errorCode,b.errorLevel,b.errorInfo,c.num,c.type,c.name FROM `wcs`.`equipment_error` a ,`wcs`.`error_type` b,`wcs`.`equipment` c WHERE a.errorType=b.id AND a.equipmentId=c.id");
            strCount.Append("SELECT COUNT(1) as countRow FROM `wcs`.`equipment_error` a ,`wcs`.`error_type` b,`wcs`.`equipment` c WHERE a.errorType=b.id AND a.equipmentId=c.id");

            if (!string.IsNullOrWhiteSpace(sbType))
            {
                strQuery.Append($" AND c.type = @sbType ");
                strCount.Append($" AND c.type = @sbType ");
            }
            if (!string.IsNullOrWhiteSpace(sbName))
            {
                strQuery.Append($" AND c.name = @sbName ");
                strCount.Append($" AND c.type = @sbType ");
            }
            if (errorCode != null)
            {
                strQuery.Append($" AND b.errorCode = @errorCode ");
                strCount.Append($" AND c.type = @sbType ");
            }
            if (errorLevel != null)
            {
                strQuery.Append($" AND b.errorLevel = @errorLevel ");
                strCount.Append($" AND c.type = @sbType ");
            }

            strQuery.Append($" LIMIT {Pageoffset},{Pagelimit}; ");

            var List = connection.Query(strQuery.ToString(), new { sbType, sbName, errorCode, errorLevel });

            var CountPage = connection.QueryFirstOrDefault(strCount.ToString(), new { sbType, sbName, errorCode, errorLevel });

            foreach (var item in CountPage)
            {
                CountPage = item.Value;
            }

            var data = List.Select(p => new { p.id, p.equipmentId, p.errorType, p.errorTime, p.errorCode, p.errorLevel, p.errorInfo, p.num, p.type, p.name });
            return new JsonData<object>() { rows = data, total = int.Parse(CountPage.ToString()) };
        }

        public bool Delete(List<Equipment_error> er)
        {
            var connection = CRUD.GetOpenConnection();
            return connection.DeleteList<Equipment_error>("WHERE id=@id", er) > 0;
        }
    }
}