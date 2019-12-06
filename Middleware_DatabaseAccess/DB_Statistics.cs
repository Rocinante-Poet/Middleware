using Dapper;
using Middleware_Tool;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Middleware_DatabaseAccess
{
    public class DB_Statistics
    {
        public IEnumerable<Chart> Get()
        {
           return CRUD.ExcuteSql(connection =>
           {
               StringBuilder sql = new StringBuilder();
               sql.Append(@"select b.name,a.value from (SELECT  equipmentId ,count(*) as value FROM equipment_error where date>=(select DATE_ADD(curdate(),interval -day(curdate())+1 day)) and  date<=
                (select last_day(curdate()))
                GROUP BY equipmentId) as a, equipment as b where a.equipmentId = b.id");
               var data = connection.Query<Chart>(sql.ToString());
               return data;
           });
        }

        public IEnumerable<Chart> GetErrorType()
        {
            return CRUD.ExcuteSql(connection =>
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(@"select a.errorcount as value,b.`errorInfo` as name 
                 from (SELECT  errorType ,count(*) as errorcount FROM equipment_error where date>=(select DATE_ADD(curdate(),interval -day(curdate())+1 day)) and  date<=(select last_day(curdate()))GROUP BY errorType) as a,error_type as b where a.errorType=b.id");
                var data = connection.Query<Chart>(sql.ToString());
                return data;
            });
        }


        public IEnumerable<ChartError<int>> GetErrorGroup()
        {
            return CRUD.ExcuteSql(connection =>
            {
                var listTime = Date.WeekTime();
                var List = connection.GetList<equipment_model>();
                Dictionary<string, ChartError<int>> pairs = new Dictionary<string, ChartError<int>>();
                foreach (var item in List)
                {
                    pairs.Add(item.name, new ChartError<int>() { name = item.name, type = "bar", data = new List<int>() { 0, 0, 0, 0, 0, 0, 0 } });
                }
                for (int i = 0; i < listTime.Count(); i++)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(@"select b.name ,a.errorcount as value FROM (SELECT equipmentId,  count(*) as errorcount from  equipment_error  WHERE date=@date GROUP BY equipmentId)  as a,equipment as b where a.equipmentId =b.id");
                    var data = connection.Query<Chart>(sql.ToString(), new { date = listTime[i].ToDate() });
                    foreach (var itemRow in data)
                    {
                        if (pairs.ContainsKey(itemRow.name))
                        {
                            pairs[itemRow.name].data[i] = itemRow.value;
                        }
                    }
                }
                return pairs.Values;
            });
        }

        public MainChart GetMainChart()
        {
            return CRUD.ExcuteSql(connection =>
            {
                MainChart chart = new MainChart();
                double awaitOrder = connection.RecordCount<order_model>("WHERE state = 0");
                double orderCount = connection.RecordCount<order_model>("WHERE state != 200 and state != 500");
                double awaitError = connection.RecordCount<equipment_error_model>("WHERE date=@date and state=-1", new { date = DateTime.Now.ToString("yyyy-MM-dd").ToDate() });
                double ErrorCount = connection.RecordCount<equipment_error_model>("WHERE date=@date", new { date = DateTime.Now.ToString("yyyy-MM-dd").ToDate() });
                chart.GetChartpercentOrder =
                new Chartpercent()
                {
                    percent = ((awaitOrder / orderCount).ToString("0.0").ToDouble() * 100).ToString(),
                    Chartdata =
                    new List<Chart>() {
                        new Chart() { name="总订单",value= orderCount.ToInt() },
                        new Chart() { name = "待执行订单", value = awaitOrder.ToInt() }
                    }
                };
                chart.GetChartpercentError =
                new Chartpercent()
                {
                    percent = ((awaitError / ErrorCount).ToString("0.0").ToDouble() * 100).ToString(),
                    Chartdata =
                    new List<Chart>() {
                        new Chart() { name="总异常",value= ErrorCount.ToInt()== 0 ? 1: ErrorCount.ToInt() },
                        new Chart() { name = "待处理异常", value = awaitError.ToInt() }
                    }
                };
                return chart;
            });
        }




    }
}
