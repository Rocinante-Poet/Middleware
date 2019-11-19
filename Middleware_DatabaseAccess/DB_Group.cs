using Dapper;
using Middleware_CoreWeb;
using Middleware_CoreWeb.cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Middleware_DatabaseAccess
{
    public class DB_Group
    {
        private string CacheKey = "Middleware_Group_Cache";

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
                CacheFactory.GetCache.Remove(CacheKey);
                return connection.ExecuteAsync(group) > 0;
            });
        }

        public bool Delete(List<group_model> grouparray)
        {
            return CRUD.ExcuteSql(connection =>
            {
                CacheFactory.GetCache.Remove(CacheKey);
                return connection.DeleteList<group_model>("WHERE id=@id", grouparray) > 0;
            });
        }

        public bool Update(group_model group)
        {
            return CRUD.ExcuteSql(connection =>
            {
                CacheFactory.GetCache.Remove(CacheKey);
                return connection.Update(group) > 0;
            });
        }

        public group_model Get(int id)
        {
            return CRUD.ExcuteSql(connection =>
            {
                var List = CacheFactory.GetCache.Get<IEnumerable<group_model>>(CacheKey);
                if (List == null)
                {
                    List = connection.GetList<group_model>();
                    CacheFactory.GetCache.Set(CacheKey, List);
                }
                return List.FirstOrDefault(p => p.id == id);
            });
        }

        public IEnumerable<group_model> Get()
        {
            return CRUD.ExcuteSql(connection =>
            {
                var List = CacheFactory.GetCache.Get<IEnumerable<group_model>>(CacheKey);
                if (List == null)
                {
                    List = connection.GetList<group_model>();
                    CacheFactory.GetCache.Set(CacheKey, List);
                }
                return List;
            });
        }
    }
}