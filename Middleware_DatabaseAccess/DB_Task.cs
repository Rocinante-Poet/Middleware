using Dapper;
using Middleware_Tool;
using System;
using System.Collections.Generic;
using System.Text;

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

        #region 统计

        public IEnumerable<Chart> GetTables(int id)
        {
            var connection = CRUD.GetOpenConnection();

            if (id == 10)
            {
                return connection.Query<Chart>("select table_name name from information_schema.tables where table_schema='wcs' and table_name regexp '^signaliolog';");
            }
            else
            {
                return connection.Query<Chart>("select table_name name from information_schema.tables where table_schema='wcs' and table_name regexp '^workshoplog';");
            }
        }

        public ChartDataPile GetChartDataWorkshop(string txt)
        {
            var connection = CRUD.GetOpenConnection();

            //Random random = new Random();
            //List<string> sr = new List<string> { "外纸箱", "小纸盒", "说明书", "空托盘", "成品", "返料" };
            //for (int i = 1; i < 1201; i++)
            //{
            //    var a = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss dddd").ToDate().AddDays(random.Next(0, 365)).AddHours(random.Next(0, 100)).AddMinutes(random.Next(0, 100)).AddSeconds(random.Next(0, 800)).ToString("yyyy-MM-dd HH:mm:ss dddd");
            //    var b = a.ToDate().AddSeconds(random.Next(200, 800)).ToString("yyyy-MM-dd HH:mm:ss dddd");
            //    var c = b.ToDate().AddSeconds(random.Next(800, 1400)).ToString("yyyy-MM-dd HH:mm:ss dddd");
            //    var y = c.ToDate().AddSeconds(random.Next(1400, 1800)).ToString("yyyy-MM-dd HH:mm:ss dddd");

            //    connection.Execute($"INSERT INTO `wcs`.`workshoplog`(`Id`, `WSnum`, `WScommand`, `WSanswer`, `WStime`, `WHPrepare`, `AGVstart`, `AGVend`) VALUES ({i}, {random.Next(9, 13)}, '{sr[random.Next(0, 6)]}', 10, '{a}', '{b}', '{c}', '{y}')");
            //}

            List<workshoplog_model> d = connection.Query<workshoplog_model>($"SELECT * FROM `wcs`.`workshoplog` WHERE `WScommand` = '{txt}' LIMIT 0,100").AsList();

            ChartDataPile cdp = new ChartDataPile();

            foreach (workshoplog_model w in d)
            {
                if (w.WSanswer == 10 || w.WSanswer == 20)
                {
                    if (w.WScommand == "成品" || w.WScommand == "返料")
                    {
                        if (!string.IsNullOrWhiteSpace(w.WStime) && !string.IsNullOrWhiteSpace(w.AGVstart) && !string.IsNullOrWhiteSpace(w.AGVend))
                        {
                            cdp.xAxisTitle.Add($"{w.WStime}");
                            cdp.timeA.Add($"{(w.AGVstart.ToDateStr() - w.WStime.ToDateStr()).TotalSeconds}".ToInt());
                            cdp.timeB.Add(0);
                            cdp.timeC.Add($"{(w.AGVend.ToDateStr() - w.AGVstart.ToDateStr()).TotalSeconds}".ToInt());
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(w.WStime) && !string.IsNullOrWhiteSpace(w.WHPrepare) && !string.IsNullOrWhiteSpace(w.AGVstart) && !string.IsNullOrWhiteSpace(w.AGVend))
                        {
                            cdp.xAxisTitle.Add($"{w.WStime}");
                            cdp.timeA.Add($"{(w.AGVstart.ToDateStr() - w.WStime.ToDateStr()).TotalSeconds}".ToInt());
                            cdp.timeB.Add($"{(w.AGVstart.ToDateStr() - w.WHPrepare.ToDateStr()).TotalSeconds}".ToInt());
                            cdp.timeC.Add($"{(w.AGVend.ToDateStr() - w.AGVstart.ToDateStr()).TotalSeconds}".ToInt());
                        }
                    }
                }
            }

            return cdp;
        }

        #endregion 统计
    }
}