using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BusRouteGuider
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartToDestination : Page
    {
        public StartToDestination()
        {
            //Navigation achieved
            Debug.WriteLine("Start to destination reached");
            //Create the GUI
            this.InitializeComponent();
        }

        //Fill the combo boxes for start location and destination
        private void fillCombo(Dictionary<String, Location> dic)
        {
            //Clear items of the combo lists for clearing any buffers
            comboStart.Items.Clear();
            comboEnd.Items.Clear();

            //The list of the combo box should appear in alphabetical order
            SortedSet<string> keySet = new SortedSet<string>();

            //Add elements from dictionary into the sorted set which conains elements in alphebetical order
            foreach (String key in dic.Keys)
            {
                keySet.Add(key);
            }

            //Fill the combo boxes
            foreach (String key in keySet)
            {
                comboStart.Items.Add(key);
                comboEnd.Items.Add(key);
            }
            Debug.WriteLine("Combo filled");

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Dictionary<String, Location> dic = e.Parameter as Dictionary<String, Location>;
            Debug.WriteLine("**********************on navigated to got called");
            this.fillCombo(dic);
        }

        private void StartToDestination_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        
    }
}
