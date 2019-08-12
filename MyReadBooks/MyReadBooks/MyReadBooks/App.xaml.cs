using Prism.Ioc;
using Prism.Unity;
using System;
using MyReadBooks.View;
using MyReadBooks.ViewModel;
using Prism;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyReadBooks
{
    public partial class App : PrismApplication
    {
        public static string DatabasePath;
        //public App(IPlatformInitializer initializer = null):base(initializer)
        //{
            
        //}

        public App(string databasePath, IPlatformInitializer initializer = null) : base(initializer)
        {
            DatabasePath = databasePath;

            NavigationService.NavigateAsync("NavigationPage/BooksPage");
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            //NavigationService.NavigateAsync("NavigationPage/BooksPage");
            // base constructor runs first, then the on Initialized, then the rest of code in the constructor 
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<BooksPage,BooksVM>();
            containerRegistry.RegisterForNavigation<NewBookPage,NewBookVM>();
            containerRegistry.RegisterForNavigation<BookDetailsPage,BookDetailsVM>();
        }
    }
}
