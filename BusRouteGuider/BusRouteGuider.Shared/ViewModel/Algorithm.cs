using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Popups;
using System.Diagnostics;


namespace BusRouteGuider.ViewModel
{
    class Algorithm
    {
        private SortedSet<String> tree;
        Dictionary<String, Location> locations;

        public Algorithm() {
            tree = new SortedSet<string>();
            locations = new Dictionary<string, Location>();
        }

        public async void getRoutes(String start, String end, Dictionary<String, Location> dic, Boolean searchAll) {
            this.locations = dic;
            findRoutes(start, start, end, new LinkedList<String>(), new LinkedList<String>(), 3);

            if (searchAll) {
                String t = "";
                int k=0;
                foreach (String s in tree) {
                    k++;
                    t = t + s + Environment.NewLine + Environment.NewLine ;
                }
                
                //Debug.WriteLine("0000000000000000000000" + k);

                MessageDialog msgbox = new MessageDialog(t);
                await msgbox.ShowAsync();
                return;
            }

        }

        public void findRoutes(String start, String current, String end, LinkedList<String> tempRoutes, LinkedList<String> tempLocations, int depth) {
            if (depth > 0) {

            if (current.Equals(end)) {

                String s = "";
                //Debug.WriteLine(tempRoutes.Count + "*****************************");
                for (int i = 0; i < tempRoutes.Count; i++) {

                    //Debug.WriteLine("+++++++++++++++++++++++++" + i );
                    if (i != 0) {
                        //Get the ith element from linkedlist tempLocations
                        LinkedListNode<String> _mark = tempLocations.First;
                        for (int p = 0; p < i; p++){
                            _mark = _mark.Next;
                        }               
                        s = s + " Drop at " + _mark.Value+". ";
                    }
                    //Get the ith element from linkedlist tempRoutes
                    LinkedListNode<String> mark = tempRoutes.First;
                    for (int p = 0; p < i; p++){
                            mark = mark.Next;
                    }   
                    s = s + "Take " + " " + mark.Value + ". ";
                }

                s = s + " Get down at your destination, " + end;
                //Debug.WriteLine("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS" + s );
                tree.Add(s);

            } else {
                Location location = locations[current];

                for (int i = 0; i < location.getRoutes().Count; i++) {

                    //Get the ith element of the linked list
                    LinkedListNode<Route> mark = location.getRoutes().First;
                    for (int p = 0; p < i; p++){
                           mark = mark.Next;
                    }  
                    Route route = mark.Value;
                    String l = locations[end].getName();
                    if (route.routeInContainsLocation(l)) {
                        Boolean isValid = true;
                        if (tempRoutes.Contains(route.getRouteNumber()) || tempLocations.Contains(location.getName())) {
                            isValid = false;
                        }
                        if (isValid) {
                            LinkedList<String> routesClone = new LinkedList<String>();
                            foreach(String s in tempRoutes){
                                 routesClone.AddLast(s);
                            }
                           
                            routesClone.AddLast(route.getRouteNumber());

                            LinkedList<String> locationsClone = new LinkedList<String>();
                            foreach(String s in tempLocations){
                                 locationsClone.AddLast(s);
                            }
                            
                            locationsClone.AddLast(location.getName());
                           
                            findRoutes(start, end, end, routesClone, locationsClone, depth);

                        }
                    } else {

                        if (!tempRoutes.Contains(route.getRouteNumber()) && !tempLocations.Contains(location.getName())) {

                            LinkedList<String> routesClone = new LinkedList<String>();
                            foreach(String s in tempRoutes){
                                 routesClone.AddLast(s);
                            }

                            routesClone.AddLast(route.getRouteNumber());

                            LinkedList<String> locationsClone = new LinkedList<String>();
                            foreach(String s in tempLocations){
                                 locationsClone.AddLast(s);
                            }
                            
                            locationsClone.AddLast(location.getName());

                            Boolean isValid = true;

                            foreach (String previous in tempLocations) {

                                foreach (Route r in locations[previous].getRoutes()) {
                                    if (r.routeInContainsLocation(location) && !locations[previous].getName().Equals(start)) {
                                        isValid = false;
                                    }
                                    if (r.routeOutContainsLocation(location) && !locations[previous].getName().Equals(start)) {
                                        isValid = false;
                                    }
                                }
                                if (previous.Equals(location.getName())) {
                                    isValid = false;
                                }
                            }

                            if (isValid) {
                                foreach (Location loc in route.getRouteIn()) {
                                   // Debug.WriteLine("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS" + routesClone.Count + "  " + locationsClone.Count);

                                    findRoutes(start, loc.getName(), end, routesClone, locationsClone, depth - 1);
                                }

                                foreach (Location loc in route.getRouteOut()) {
                                   // Debug.WriteLine("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS" + routesClone.Count + "  " + locationsClone.Count);

                                    findRoutes(start, loc.getName(), end, routesClone, locationsClone, depth - 1);
                                }
                            }
                        }
                    }
                }
            }
        }
        }
    }
}

