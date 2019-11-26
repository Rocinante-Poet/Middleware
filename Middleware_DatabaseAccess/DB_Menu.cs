using Dapper;
using Middleware_CoreWeb;
using Middleware_Tool;
using Middleware_Tool.cache;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware_DatabaseAccess
{
    public class DB_Menu
    {
        private string CacheMenuKey = "Middleware_Menu_Cache";

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Pagelimit"></param>
        /// <param name="Pageoffset"></param>
        /// <param name="meunName"></param>
        /// <returns></returns>
        public async Task<JsonData<menu_model>> GetList(int Pagelimit, int Pageoffset, string meunName)
        {
            var connection = CRUD.GetOpenConnection();

            StringBuilder strQuery = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(meunName))
            {
                strQuery.Append("where name like @Name");
            }
            var List = await connection.GetListPagedAsync<menu_model>((Pageoffset / Pagelimit) + 1, Pagelimit, strQuery.ToString(), "", new { Name = $"%{meunName}%" });
            var CountPage = await connection.RecordCountAsync<menu_model>(strQuery.ToString(), new { Name = $"%{meunName}%" });
            return new JsonData<menu_model>() { rows = List, total = CountPage };
        }

        public IEnumerable<menu_model> GetFatherLists()
        {
            var List = CacheFactory.GetCache.Get<IEnumerable<menu_model>>(CacheMenuKey);
            if (List == null)
            {
                var connection = CRUD.GetOpenConnection();
                List = connection.GetList<menu_model>();
                CacheFactory.GetCache.Set(CacheMenuKey, List);
            }
            return List.Where(p => p.menuid == 0).OrderBy(p => p.no);
        }

        public List<NavigatorItem> GetFatherList()
        {
            List<NavigatorItem> navigatorItems = new List<NavigatorItem>();
            var List = CacheFactory.GetCache.Get<IEnumerable<menu_model>>(CacheMenuKey);
            if (List == null)
            {
                var connection = CRUD.GetOpenConnection();
                List = connection.GetList<menu_model>();
                CacheFactory.GetCache.Set(CacheMenuKey, List);
            }
            foreach (var item in List)
            {
                if (item.menuid == 0)
                {
                    navigatorItems.Add(new NavigatorItem() { FatherItem = item, sonMenuItem = List.Where(p => p.menuid == item.id).Where(p => p != null) });
                }
            }
            return navigatorItems;
        }

        public List<NavigatorItem> NavigatorBarList(int GroupID)
        {
            List<NavigatorItem> navigatorItems = new List<NavigatorItem>();
            IEnumerable<menu_model> detailList = new DB_detail().Get(GroupID).Select(p => this.Get(p.menuid)).Where(p => p != null).OrderBy(p => p.no);
            foreach (var item in detailList)
            {
                if (item.menuid == 0)
                {
                    navigatorItems.Add(new NavigatorItem() { FatherItem = item, sonMenuItem = detailList.Where(p => p.menuid == item.id).Where(p => p != null) });
                }
            }
            return navigatorItems;
        }



        public bool AddMenu(menu_model menu)
        {
            return CRUD.ExcuteSql(connection =>
            {
                CacheFactory.GetCache.Remove(CacheMenuKey);
                return connection.ExecuteAsync(menu) > 0;
            });
        }

        public bool DeleteMenu(List<menu_model> menuarray)
        {
            return CRUD.ExcuteSql(connection =>
            {
                CacheFactory.GetCache.Remove(CacheMenuKey);
                return connection.DeleteList<menu_model>("WHERE id=@id", menuarray) > 0;
            });
        }

        public bool UpdateMenu(menu_model menu)
        {
            return CRUD.ExcuteSql(connection =>
            {
                CacheFactory.GetCache.Remove(CacheMenuKey);
                return connection.Update(menu) > 0;
            });
        }

        public menu_model Get(int id)
        {

            var List = CacheFactory.GetCache.Get<IEnumerable<menu_model>>(CacheMenuKey);
            if (List == null)
            {
                var connection = CRUD.GetOpenConnection();
                List = connection.GetList<menu_model>();
                CacheFactory.GetCache.Set(CacheMenuKey, List);
            }
            return List.FirstOrDefault(p => p.id == id);
        }

        public IEnumerable<menu_model> Get()
        {
            var List = CacheFactory.GetCache.Get<IEnumerable<menu_model>>(CacheMenuKey);
            if (List == null)
            {
                var connection = CRUD.GetOpenConnection();
                List = connection.GetList<menu_model>();
                CacheFactory.GetCache.Set(CacheMenuKey, List);
            }
            return List;
        }
    }
}