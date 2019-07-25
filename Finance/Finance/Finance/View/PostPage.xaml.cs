using System;
using System.Collections.Generic;
using Finance.Model;
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

            webView.Source = item.ItemLink;
        }
    }
}
