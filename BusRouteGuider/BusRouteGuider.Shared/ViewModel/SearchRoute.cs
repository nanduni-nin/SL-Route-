using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using Windows.Storage;
using System.Threading.Tasks;
using System.Windows;



namespace BusRouteGuider.ViewModel
{
    class SearchRoute
    {
        private static Dictionary<String, Route> routes;
        private static Dictionary<String, Location> locations;
        private static SortedSet<string> set; 
        private static int minCount;
        private static int maxCount;
        private static String[] path;

        public SearchRoute() {            
            minCount = 5;
            maxCount = 0;
          //  start();
        }

        public void start() { 
            routes = new Dictionary<String, Route>();
            locations = new Dictionary<String, Location>();
            //path = new String[500];
            set = new SortedSet<string>(); 
            Debug.WriteLine("++++++++++++++++++++++++");
        }


        public async void loadData() {
            //int p =0;
            Debug.WriteLine("Load Data Came");
            String[] lines;
            String[] temp;
            Route route;

            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await folder.GetFileAsync("data.txt");
            var contents =  await Windows.Storage.FileIO.ReadTextAsync(file);

            lines = contents.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

           
            /*string line = "100|fort,moratuwa|moratuwa,fort";
            temp = line.Split('|');
            route = new Route(temp[0]);
            route = processInput(temp[1], route);
            route = processInput(temp[2], route);
            routes.Add(temp[0], route);
            */

            
            for (int i = 0; i < lines.Length; i++)
            {
                temp = lines[i].Split('|');
                route = new Route(temp[0]);
                //Debug.WriteLine(temp[0] + "-------" + temp[1] + "------------" + temp[2]);
                route = processInput(temp[1], route);
                route = processInput(temp[2], route);
                routes.Add(temp[0], route);
            }

        }

             

        private static Route processInput(String temp, Route route) {
            Location location = null;
            String[] tempLocations = temp.Split(',');

            foreach (String s in tempLocations) {
                if (!locations.ContainsKey(s)) {
                    location = new Location(s);
                    route.addLocationToRouteIn(location);
                    locations.Add(s, location);
                } else {
                    Location value;
                    if (locations.TryGetValue(s, out value)) {
                        route.addLocationToRouteIn(locations[s]);
                    }
                }
            }
            return route;
        }

        public Dictionary<String, Location> getAllLocations() {
            return locations;
        }

        public Dictionary<String, Route> getAllRoutes() {
            return routes;
        }
    }
}
