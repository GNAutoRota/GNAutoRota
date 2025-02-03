using Foundation;
using UIKit;
using Firebase.Core;

namespace GNAutoRota
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Inicialize o Firebase
            Firebase.Core.App.Configure();

            // Outras configurações, se necessário
            return base.FinishedLaunching(application, launchOptions);
        }
    }
}
