using Dapper;
using Middleware_CoreWeb;
using Middleware_Tool;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Middleware_DatabaseAccess
{
    public class DB_Task
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Pagelimit"></param>
        /// <param name="Pageoffset"></param>
        /// <param name="meunName"></param>
        /// <returns></returns>
        public JsonData<Transportndc_model> GetList(int Pagelimit, int Pageoffset)
        {
            var connection = CRUD.GetOpenConnection();

            StringBuilder strQuery = new StringBuilder();

            var List = connection.GetListPaged<Transportndc_model>((Pageoffset / Pagelimit) + 1, Pagelimit, strQuery.ToString(), "");

            var CountPage = connection.RecordCount<Transportndc_model>(strQuery.ToString());
            return new JsonData<Transportndc_model>()
            {
                rows = List,
                total = CountPage
            };
        }

        public bool Add(Transportndc_model ndc)
        {
            var connection = CRUD.GetOpenConnection();
            return connection.InsertAsync(ndc) != null;
        }

        public bool Delete(List<Transportndc_model> ndc)
        {
            var connection = CRUD.GetOpenConnection();
            return connection.DeleteList<Transportndc_model>("WHERE id=@id", ndc) > 0;
        }

        public bool Update(Transportndc_model ndc)
        {
            var connection = CRUD.GetOpenConnection();
            return connection.Update(ndc) > 0;
        }
    }
}