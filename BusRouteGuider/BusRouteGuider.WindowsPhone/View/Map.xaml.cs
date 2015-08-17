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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace BusRouteGuider
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Map : Page
    {
        public Map()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void getPositionButton_Click(object sender, RoutedEventArgs e)
        {
            positionTextBlock.Text = String.Format("{0}, {1}",
            MyMap.Center.Position.Latitude,
            MyMap.Center.Position.Longitude);
        }

        private async void setPositionButton_Click(object sender, RoutedEventArgs e)
        {
            var myPosition = new Windows.Devices.Geolocation.BasicGeoposition();
            myPosition.Latitude = 41.7446;
            myPosition.Longitude = -087.7915;

            var myPoint = new Windows.Devices.Geolocation.Geopoint(myPosition);
            if (await MyMap.TrySetViewAsync(myPoint, 10D))
            {
                // Haven't really thought that through!
            }
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (MyMap != null)
                MyMap.ZoomLevel = e.NewValue;
        }
    }
}
