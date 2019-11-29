using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_Tool
{
    public class Date
    {

        public string Week()
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];
            return week;
        }

        public static List<string> WeekTime()
        {
            List<string> TimeList = new List<string>();
            switch (Convert.ToInt32(DateTime.Now.DayOfWeek))
            {
                case 0:
                    for (int i = 0; i < 7; i++)
                    {
                        TimeList.Add(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd"));
                    }
                    break;
                case 1:
                    for (int i = 0; i < 7; i++)
                    {
                        TimeList.Add(DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                    }
                    break;
                case 2:
                    for (int i = 0; i < 7; i++)
                    {
                        if (i == 0)
                            TimeList.Add(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
                        else
                            TimeList.Add(DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                    }
                    break;
                case 3:
                    for (int i = 1; i < 3; i++)
                    {
                        TimeList.Add(DateTime.Now.AddDays(-(3 - i)).ToString("yyyy-MM-dd"));
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        TimeList.Add(DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                    }
                    break;
                case 4:
                    for (int i = 1; i < 4; i++)
                    {
                        TimeList.Add(DateTime.Now.AddDays(-(4 - i)).ToString("yyyy-MM-dd"));
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        TimeList.Add(DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                    }
                    break;
                case 5:
                    for (int i = 1; i < 5; i++)
                    {
                        TimeList.Add(DateTime.Now.AddDays(-(5 - i)).ToString("yyyy-MM-dd"));
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        TimeList.Add(DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                    }
                    break;
                case 6:
                    for (int i = 1; i < 6; i++)
                    {
                        TimeList.Add(DateTime.Now.AddDays(-(6 - i)).ToString("yyyy-MM-dd"));
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        TimeList.Add(DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                    }
                    break;
            }

            return TimeList;
        }
    }
}
