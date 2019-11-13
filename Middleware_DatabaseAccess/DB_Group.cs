using Dapper;
using Middleware_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_DatabaseAccess
{
    public class DB_Group
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Pagelimit"></param>
        /// <param name="Pageoffset"></param>
        /// <param name="meunName"></param>
        /// <returns></returns>
        public JsonData<group_model> GetList(int Pagelimit, int Pageoffset, string meunName)
        {
            return CRUD.ExcuteSql((connection) =>
            {
                StringBuilder strQuery = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(meunName))
                {
                    strQuery.Append("where name like @Name");
                }
                var List = connection.GetListPaged<group_model>((Pageoffset / Pagelimit) + 1, Pagelimit, strQuery.ToString(), "", new { Name = $"%{meunName}%" });
                var CountPage = connection.RecordCount<group_model>(strQuery.ToString(), new { Name = $"%{meunName}%" });
                return new JsonData<group_model>() { rows = List, total = CountPage };
            });
        }

        public bool Add(group_model group)
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.Insert(group) > 0;
            });
        }

        public bool Delete(List<group_model> grouparray)
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.DeleteList<group_model>("WHERE id=@id", grouparray) > 0;
            });
        }

        public bool Update(group_model group)
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.Update(group) > 0;
            });
        }


        public group_model Get(int id)
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.Get<group_model>(id);
            });
        }

        public IEnumerable<group_model> Get()
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.GetList<group_model>();
            });
        }
    }
}
