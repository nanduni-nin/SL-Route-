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
using Windows.UI.Popups;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace BusRouteGuider
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartToDestination : Page
    {
        Dictionary<String, Location> dic;
        BusRouteGuider.ViewModel.Algorithm process;


        public StartToDestination()
        {
            //Navigation achieved
            Debug.WriteLine("Start to destination reached");
            //Create the GUI
            this.InitializeComponent();
            //Set functionality for the phone's back buttons
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            //Create an object from Algorithm Class
            process = new ViewModel.Algorithm();
        }

        //Fill the combo boxes for start location and destination
        private void fillCombo(Dictionary<String,Location> dic)
        {
            //Clear items of the combo lists for clearing any buffers
            comboStart.Items.Clear();
            comboEnd.Items.Clear();

            //The list of the combo box should appear in alphabetical order
            SortedSet<string> keySet = new SortedSet<string>();

            //Add elements from dictionary into the sorted set which conains elements in alphebetical order
            foreach (String key in dic.Keys){
                keySet.Add(key);
            }
            
            //Fill the combo boxes
            foreach (String key in keySet){
                comboStart.Items.Add(key);
                comboEnd.Items.Add(key);
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
            this.dic = dic;
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

        private async void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((comboStart.SelectedItem.ToString()).Equals(comboEnd.SelectedItem.ToString()))
            {
                MessageDialog msgbox = new MessageDialog("Enter different locations for Start and Destination");
                await msgbox.ShowAsync();
                return;
            }
            else {
                process.getRoutes(comboStart.SelectedItem.ToString(), comboEnd.SelectedItem.ToString(), dic, true);
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            //return to main page
            this.Frame.Navigate(typeof(MainPage));
        }

        

        

        

        
    }
}
