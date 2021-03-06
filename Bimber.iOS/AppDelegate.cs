using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Bimber.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            Xamarin.Forms.Nuke.FormsHandler.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
