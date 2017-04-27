using com.kmd.Models;
using com.kmd.Services;
using com.kmd.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace com.kmd {
    public partial class App : Application {

        public static  int ScreenWidth { get; set; }
        public static  int ScreenHeight { get; set; }
        public static  float ScreenDensity { get; set; }

        public App () {
            InitializeComponent ();

            SetMainPage ();
        }

        public static void SetMainPage () {
            Current.MainPage = new TabbedPage {
                Children =
                {
                    //new NavigationPage(new ItemsPage())
                    //{
                    //    Title = "Tasks",
                    //    Icon = Device.OnPlatform("tab_feed.png", null, null)
                    //},
                    new NavigationPage(new ProgressPage())
                    {
                        Title = "Tasks",
                        Icon = Device.OnPlatform("tab_feed.png", null, null),
                        
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform("tab_about.png", null, null)
                    },
                }
            };
        }

        protected override void OnSleep () {
            base.OnSleep ();
            DependencyService.Get<IDataStore<Item>> ().SyncAsync ();
        }

        protected override void OnResume () {
            base.OnResume ();
            DependencyService.Get<IDataStore<Item>> ().PullLatestAsync();
        }

        protected override void OnStart () {
            base.OnStart ();
            DependencyService.Get<IDataStore<Item>> ().PullLatestAsync ();
        }

    }
}
