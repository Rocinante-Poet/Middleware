using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_Tool
{
    public class Chart
    {
        public string name { get; set; }

        public int value { get; set; }
    }

    public class Chartpercent
    {
        public string percent { get; set; }
        public IEnumerable<Chart> Chartdata { get; set; }
    }

    public class MainChart
    {
        public Chartpercent GetChartpercentOrder { get; set; }

        public Chartpercent GetChartpercentError { get; set; }
    }

    public class ChartDataPile
    {
        public ChartDataPile()
        {
            xAxisTitle = new List<string>();
            timeA = new List<int>();
            timeB = new List<int>();
            timeC = new List<int>();
        }

        public List<string> xAxisTitle { get; set; }

        public List<int> timeA { get; set; }

        public List<int> timeB { get; set; }

        public List<int> timeC { get; set; }
    }
}