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
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace BusRouteGuider
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShowRoutes : Page
    {
        BusRouteGuider.ViewModel.SearchRoute processor;

        public ShowRoutes()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            processor = new ViewModel.SearchRoute();

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Debug.WriteLine("**********************on navigated to got called");
            processor.start();
            processor.loadData();
            this.fillList();
        }

        //Fill the list box from the data from the data file
        public void fillList() {            
            foreach(String s in processor.getAllRoutes().Keys)
            {
               // String detail = s + " | " + (processor.getAllRoutes())[s].getPath();
              //  ListRoutes.Items.Add(detail);
                Debug.WriteLine("List filled **********");

            }
            Debug.WriteLine("List filled");
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
            this.Frame.Navigate(typeof(MainPage));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }

}
