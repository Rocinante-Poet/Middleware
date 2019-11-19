using Dapper;
using Middleware_CoreWeb;
using Middleware_CoreWeb.cache;
using Middleware_Tool;
using System.Collections.Generic;
using System.Linq;

namespace Middleware_DatabaseAccess
{
    public class DB_detail
    {
        private string CachedetailKey = "Middleware_detail_Cache";


        public IEnumerable<detail_model> Get(int groupid)
        {
            var List = CacheFactory.GetCache.Get<IEnumerable<detail_model>>(CachedetailKey);
            if (List == null)
            {
                var connection = CRUD.GetOpenConnection();
                List = connection.GetList<detail_model>();
                CacheFactory.GetCache.Set(CachedetailKey, List);
            }
            return List.Where(p => p.groupid == groupid);
        }


        public bool Add(IEnumerable<menu_model> detailList, int id)
        {
            return CRUD.ExcuteSql((connection, transaction) =>
            {
                CacheFactory.GetCache.Remove(CachedetailKey);
                var list = new List<detail_model>();
                detailList.ToList().ForEach(p => list.Add(new detail_model() { groupid = id, menuid = p.id }));
                if (list.Count() > 0)
                {
                    connection.DeleteList<detail_model>("WHERE groupid=@groupid", new { groupid = id }, transaction);
                    return connection.Execute("insert into detail(groupid,menuid) values(@groupid, @menuid)", list, transaction) > 0;
                }
                else
                {
                    connection.DeleteList<detail_model>("WHERE groupid=@groupid", new { groupid = id }, transaction);
                    return true;
                }
            });
        }


        public List<NavigatorItem> NavigatorBarList(int GroupID)
        {
            List<NavigatorItem> navigatorItems = new List<NavigatorItem>();
            IEnumerable<menu_model> detailList = this.Get(GroupID).Select(p => new DB_Menu().Get(p.menuid)).Where(p => p != null).OrderBy(p => p.no);
            foreach (var item in detailList)
            {
                if (item.menuid == 0)
                {
                    navigatorItems.Add(new NavigatorItem() { FatherItem = item, sonMenuItem = detailList.Where(p => p.menuid == item.id).Where(p => p != null) });
                }
            }
            return navigatorItems;
        }
    }
}
