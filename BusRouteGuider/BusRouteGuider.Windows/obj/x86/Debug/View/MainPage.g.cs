﻿

#pragma checksum "C:\Users\Nands\documents\visual studio 2013\Projects\BusRouteGuider\BusRouteGuider\BusRouteGuider.Windows\View\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D3E9DAB6BC1C9B43841E36C70EC24CE5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusRouteGuider
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 11 "..\..\..\View\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.StartToDestination_ItemClick;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 14 "..\..\..\View\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.StartToDestination_ItemClick;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 19 "..\..\..\View\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.StartToDestination_ItemClick;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 30 "..\..\..\View\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.CurrentLocationToDestination_ItemClick;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 42 "..\..\..\View\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.RouteNumber_ItemClick;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 46 "..\..\..\View\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.BusesAtCurrentLocation_ItemClick;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


