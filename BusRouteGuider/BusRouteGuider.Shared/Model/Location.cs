using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace BusRouteGuider
{
    class Location
    {
        private String name;
        private LinkedList<Route> routes;

        public Location(String name) {
        routes = new LinkedList<Route>();
        this.name = name;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public void addRoute(Route route) {
        if (!routes.Contains(route)) {
            routes.AddLast(route);
        }
    }

    public LinkedList<Route> getRoutes() {
        return routes;
    }

    //public Route getRoute(int i) {
    //    //Get the ith element of the linked list
    //    Route mark = (Route)(routes.First);
    //    for (int p = 0; p < i; p++)
    //    {
    //        mark = mark.Next;
    //    }
    //    return mark;
    //}

    public void printData() {        
        Debug.WriteLine("Location: " + name);
        foreach (Route route in routes) {
            Debug.WriteLine(route.getRouteNumber());
        }
        Debug.WriteLine("");
    }

    
    public String toString() {
        return name;
    }

    }
}
