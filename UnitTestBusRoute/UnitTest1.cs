using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using BusRouteGuider.ViewModel;

namespace UnitTestBusRoute
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void processData()
        {
            BusRouteGuider.RoutesViaLocation std = new BusRouteGuider.RoutesViaLocation();
            int sum = (int)(Math.Abs(3.45));
            Assert.AreEqual(0,sum);
           
        }
    }
}
