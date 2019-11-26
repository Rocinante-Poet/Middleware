using Dapper;
using Middleware_Tool;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Middleware_DatabaseAccess
{
    public class DB_Order
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Pagelimit"></param>
        /// <param name="Pageoffset"></param>
        /// <param name="meunName"></param>
        /// <returns></returns>
        public async Task<JsonData<order_model>> GetList(int Pagelimit, int Pageoffset, string boxId)
        {
            var connection = CRUD.GetOpenConnection();
            StringBuilder strQuery = new StringBuilder();
            int boxid = boxId.ToInt();
            if (boxid != 0)
            {
                strQuery.Append("where boxId like @boxId");
            }
            var List = await connection.GetListPagedAsync<order_model>((Pageoffset / Pagelimit) + 1, Pagelimit, strQuery.ToString(), "id desc ", new { boxId = boxid });
            var CountPage = await connection.RecordCountAsync<order_model>(strQuery.ToString(), new { boxId = boxid });
            return new JsonData<order_model>() { rows = List, total = CountPage };
        }

        public bool Add(order_model group)
        {
            return CRUD.ExcuteSql(connection =>
            {
                group.createTime = DateTime.Now;
                return connection.ExecuteAsync(group) > 0;
            });
        }

        public bool Delete(List<order_model> grouparray)
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.DeleteList<order_model>("WHERE id=@id", grouparray) > 0;
            });
        }

        public bool Update(order_model group)
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.Update(group) > 0;
            });
        }
    }
}