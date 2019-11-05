using Dapper;
using Middleware_Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware_Model
{
    public class DB_Menu
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<menu_model> GetFatherList()
        {
            return CRUD.ExcuteSql<IEnumerable<menu_model>>((connection) =>
            {
                return connection.GetList<menu_model>(new { menuid = 0 });
            });
        }

        public IEnumerable<menu_model> sonItemMenu(int Id)
        {
            return CRUD.ExcuteSql<IEnumerable<menu_model>>(connection =>
            {
                return connection.GetList<menu_model>(new { menuid = Id });
            });
        }
    }
}