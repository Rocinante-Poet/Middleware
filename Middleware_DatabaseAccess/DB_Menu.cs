using Dapper;
using Middleware_Model;
using Middleware_Tool;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var connection = CRUD.GetOpenConnection();

            StringBuilder strQuery = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(meunName))
            {
                strQuery.Append("where name like @Name");
            }
            var List = connection.GetListPaged<menu_model>((Pageoffset / Pagelimit) + 1, Pagelimit, strQuery.ToString(), "", new { Name = $"%{meunName}%" });
            var CountPage = connection.RecordCount<menu_model>(strQuery.ToString(), new { Name = $"%{meunName}%" });
            return new JsonData<menu_model>() { rows = List, total = CountPage };
        }

        public IEnumerable<menu_model> GetFatherList()
        {
            var connection = CRUD.GetOpenConnection();
            return connection.GetList<menu_model>(new { menuid = 0 }).OrderBy(p => p.no);
        }

        public IEnumerable<menu_model> sonItemMenu(int Id)
        {
            var connection = CRUD.GetOpenConnection();
            return connection.GetList<menu_model>(new { menuid = Id }).OrderBy(p => p.no);
        }
    }
}