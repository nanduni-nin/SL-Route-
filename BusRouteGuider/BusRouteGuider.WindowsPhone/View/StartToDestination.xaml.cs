using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using System.Diagnostics;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace BusRouteGuider
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartToDestination : Page
    {
        //BusRouteGuider.ViewModel.SearchRoute processor;
        //BusRouteGuider.ViewModel.SearchRoute processor;
       // Dictionary<String, Location> dic;
        public StartToDestination()
        {
            Debug.WriteLine("Start to destination reached");
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            //processor = new ViewModel.SearchRoute();
            //processor = processor1;
           // this.dic = dic;
            /*
            while(comboStart.Items.Count==0 && count <100){
                this.fillCombo();
                count++;
            }*/
        }

        //Fill the combo boxes for start location and destination
        private void fillCombo(Dictionary<String,Location> dic)
        {
            int i = 0;
            comboStart.Items.Clear();
            comboEnd.Items.Clear();
            
            foreach (Location loc in dic.Values){
               comboStart.Items.Add(loc.getName());
               //Debug.WriteLine("Added to ComboStart");
               comboEnd.Items.Add(loc.getName());
               Debug.WriteLine("Added to ComboEnd" + comboEnd.Items.ElementAt(i));
               i++;
           }
            Debug.WriteLine("Combo filled");
           
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Dictionary<String, Location> dic = e.Parameter as Dictionary<String, Location>;
            Debug.WriteLine("**********************on navigated to got called");
            //processor.start();
          //  processor.loadData();
            this.fillCombo(dic);
            
        }

        //Set the functionality for the phone's back button to move one page back
        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
                e.Handled = true;
            }
        }

        private void Menu_ItemClick(object sender, ItemClickEventArgs e)
        {
            //return to main page
            this.Frame.Navigate(typeof(MainPage));
        }

        

        

        
    }
}
