using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin;
using Microsoft.Extensions.Logging;

namespace GNAutoRota
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif


            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
            { 
                ApiKey = "AIzaSyAZ_nfIgxri-xNGEM6tXQVAYX6lfX_7PTY",
                AuthDomain = "gnautorota.firebaseapp.com",
                Providers = [new EmailProvider()]
            }));
            return builder.Build();
        }
    }
}
