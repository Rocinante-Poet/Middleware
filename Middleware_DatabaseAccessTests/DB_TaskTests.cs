using Microsoft.VisualStudio.TestTools.UnitTesting;
using Middleware_DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_Tests
{
    [TestClass()]
    public class DB_TaskTests
    {
        [TestMethod()]
        public void GetChartDataWorkshopTest()
        {
            new DB_Task().GetChartDataWorkshop("workshoplog1911");
        }
    }
}