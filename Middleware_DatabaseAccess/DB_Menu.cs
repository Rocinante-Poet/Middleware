using Dapper;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Middleware_Model;
using Middleware_Tool.cache;

namespace Middleware_DatabaseAccess
{
    public class DB_Menu
    {
        private string menuCacheKey = "Middleware_Menu_Cache";

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Pagelimit"></param>
        /// <param name="Pageoffset"></param>
        /// <param name="meunName"></param>
        /// <returns></returns>
        public async Task<JsonData<menu_model>> GetList(int Pagelimit, int Pageoffset, string meunName)
        {
            return await CRUD.ExcuteSqlAsync(async (connection) =>
            {
                StringBuilder strQuery = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(meunName))
                {
                    strQuery.Append("where name like @Name");
                }
                var List = await connection.GetListPagedAsync<menu_model>((Pageoffset / Pagelimit) + 1, Pagelimit, strQuery.ToString(), "", new { Name = $"%{meunName}%" });
                var CountPage = await connection.RecordCountAsync<menu_model>(strQuery.ToString(), new { Name = $"%{meunName}%" });
                return new JsonData<menu_model>() { rows = List, total = CountPage };
            });
        }

        public IEnumerable<menu_model> GetFatherList()
        {
            return CRUD.ExcuteSql((connection) =>
            {
                var List = CacheFactory.GetCache.Get<IEnumerable<menu_model>>(menuCacheKey);
                if (List == null)
                {
                    List = connection.GetList<menu_model>();
                    CacheFactory.GetCache.Set(menuCacheKey, List);
                }
                return List.Where(p => p.menuid == 0).OrderBy(p => p.no);
            });
        }

        public IEnumerable<menu_model> sonItemMenu(int Id)
        {
            return CRUD.ExcuteSql(connection =>
            {
                var List = CacheFactory.GetCache.Get<IEnumerable<menu_model>>(menuCacheKey);
                if (List == null)
                {
                    List = connection.GetList<menu_model>();
                    CacheFactory.GetCache.Set(menuCacheKey, List);
                }
                return List.Where(p => p.menuid == Id).OrderBy(p => p.no);
            });
        }

        public async Task<bool> AddMenu(menu_model menu)
        {
            return await CRUD.ExcuteSqlAsync(async connection =>
            {
                CacheFactory.GetCache.Remove(menuCacheKey);
                return await connection.InsertAsync(menu) > 0;
            });
        }

        public async Task<bool> DeleteMenu(List<menu_model> menuarray)
        {
            return await CRUD.ExcuteSqlAsync(async connection =>
            {
                CacheFactory.GetCache.Remove(menuCacheKey);
                return await connection.DeleteListAsync<menu_model>("WHERE id=@id", menuarray) > 0;
            });
        }

        public async Task<bool> UpdateMenu(menu_model menu)
        {
            return await CRUD.ExcuteSqlAsync(async connection =>
            {
                CacheFactory.GetCache.Remove(menuCacheKey);
                return await connection.UpdateAsync(menu) > 0;
            });
        }
    }
}
