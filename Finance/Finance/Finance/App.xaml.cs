using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Finance.View;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Finance
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("android=d2808417-4efe-42de-ba80-237001d364f1;" +
                            "ios=15034a68-c0f0-49b0-b9b4-a4325f588d7c",
                typeof(Analytics), typeof(Crashes), typeof(Push));
            // we can put this line between InitializeComponent and MainPage assignment if we wanted to start collecting crashes earlier

            handleCrash();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private async void handleCrash()
        {
            bool didAppCrash = await Crashes.HasCrashedInLastSessionAsync();
            if (didAppCrash)
            {
                var crashReport = await Crashes.GetLastSessionCrashReportAsync();// we can use the report data to give the user more info or do some action
            }
        }
    }
}
