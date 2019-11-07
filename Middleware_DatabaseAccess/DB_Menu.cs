using Dapper;
using Middleware_DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Middleware_Model;

namespace Middleware_DatabaseAccess
{
    public class DB_Menu
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Pagelimit"></param>
        /// <param name="Pageoffset"></param>
        /// <param name="meunName"></param>
        /// <returns></returns>
        public JsonData<menu_model> GetList(int Pagelimit, int Pageoffset, string meunName)
        {
            return CRUD.ExcuteSql((connection) =>
            {
                StringBuilder strQuery = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(meunName))
                {
                    strQuery.Append("where name like CONCAT('%',@Name,'%')"); 
                }
                var List = connection.GetListPaged<menu_model>((Pageoffset / Pagelimit) + 1, Pagelimit, strQuery.ToString(), "", new { Name = meunName });
                var CountPage = connection.RecordCount<menu_model>(strQuery.ToString(),new { Name = meunName });
                return new JsonData<menu_model>() { rows = List, total = CountPage };
            });
        }

        public IEnumerable<menu_model> GetFatherList()
        {
            return CRUD.ExcuteSql<IEnumerable<menu_model>>((connection) =>
            {
                return connection.GetList<menu_model>(new { menuid = 0 }).OrderBy(p => p.no);
            });
        }

        public IEnumerable<menu_model> sonItemMenu(int Id)
        {
            return CRUD.ExcuteSql<IEnumerable<menu_model>>(connection =>
            {
                return connection.GetList<menu_model>(new { menuid = Id }).OrderBy(p => p.no);
            });
        }
    }
}
