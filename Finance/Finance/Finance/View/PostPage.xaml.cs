using System;
using System.Collections.Generic;
using Finance.Model;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace Finance.View
{
    public partial class PostPage : ContentPage
    {
        public PostPage()
        {
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);
            //reserves the home button area at the buttom for newer iphones without content
        }

        public PostPage(Item item)
        {
            InitializeComponent();

            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);
            //reserves the home button area at the buttom for newer iphones without content
            try
            {
                var properties = new Dictionary<string, string>()
                {
                    {"Blog_Post", item.Title}
                };
                webView.Source = item.ItemLink;
                throw (new Exception("unable to load blog"));
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>()
                {
                    {"Blog_Post", item.Title}
                };
                TrackError(ex, properties);
                Crashes.GenerateTestCrash(); // this will actually crash the app so next time HasCrashedInLastSessionAsync would return true
            }
            
        }

        private async void TrackEvent(Dictionary<string, string> properties)
        {
            if (await Analytics.IsEnabledAsync())
            {
                Analytics.TrackEvent("Blog_Post_Being_Opened", properties);
            }
        }

        private async void TrackError(Exception ex,Dictionary<string, string> properties)
        {
            if (await Crashes.IsEnabledAsync())
            {
                Crashes.TrackError(ex, properties);
            }
        }
    }
}
