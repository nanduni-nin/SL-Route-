using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UnitTestBusRoute
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void processData()
        {

            String content = "100|Panadura,Walana,Old Galle Road,Keselwatta,Moratuwa,Ratmalana,Mt. Lavinia (Galkissa),Dehiwala,Wellawatte,Bambalapitiya,Kollupitiya,Galle Face,Fort,Pettah|Pettah,Fort,Galle Face,Kollupitiya,Bambalapitiya,Wellawatte,Dehiwala,Mt. Lavinia (Galkissa),Ratmalana,Moratuwa,Keselwatta,Old Galle Road,Walana,Panadura";

            String routeNumber = "100";
            String routeIn = "Panadura,Walana,Old Galle Road,Keselwatta,Moratuwa,Ratmalana,Mt. Lavinia (Galkissa),Dehiwala,Wellawatte,Bambalapitiya,Kollupitiya,Galle Face,Fort,Pettah";
            String routeOut = "|Pettah,Fort,Galle Face,Kollupitiya,Bambalapitiya,Wellawatte,Dehiwala,Mt. Lavinia (Galkissa),Ratmalana,Moratuwa,Keselwatta,Old Galle Road,Walana,Panadura";

            BusRouteGuider.ViewModel.SearchRoute t;;
            

            
            //assert
            String[] actual;
            actual[0] = routeNumber;
            actual[1] = routeIn;
            actual[2] = routeOut;

            String after = "";
            after = after + routeNumber;
            after = after + routeIn;
            after = after + routeOut;

           
            Assert.AreEqual(content, after, "Routes are properly added to the dictionary");
        }
    }
}
