
using Foundation;
using UIKit;

namespace com.kmd.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new App());

            App.ScreenWidth = (int) UIScreen.MainScreen.Bounds.Width;
            App.ScreenHeight = (int) UIScreen.MainScreen.Bounds.Height;
            App.ScreenDensity = (float) UIScreen.MainScreen.Scale;

            return base.FinishedLaunching(app, options);
		}

        public override void AwakeFromNib () {
            base.AwakeFromNib ();
            App.ScreenWidth = (int) UIScreen.MainScreen.Bounds.Width;
            App.ScreenHeight = (int) UIScreen.MainScreen.Bounds.Height;
            App.ScreenDensity = (float) UIScreen.MainScreen.Scale;
        }

        public override void OnActivated (UIApplication uiApplication) {
            base.OnActivated (uiApplication);
            App.ScreenWidth = (int) UIScreen.MainScreen.Bounds.Width;
            App.ScreenHeight = (int) UIScreen.MainScreen.Bounds.Height;
            App.ScreenDensity = (float) UIScreen.MainScreen.Scale;
        }
    }
}
