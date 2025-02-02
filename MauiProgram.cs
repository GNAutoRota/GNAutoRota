using Microsoft.Extensions.Logging;
using GNAutoRota.Auth;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Firebase.Auth;

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

            var firebaseauthclient = new FireBaseAuthClientInjecao();

            //var authClient = firebaseauthclient.ExecutaInjecaoServico();

            //builder.Services.AddSingleton(authClient);

            //builder.Services.AddSingleton<IFirebaseAuth>(FirebaseAuth);

            CrossFirebaseAuth.Current.SignInWithEmailAndPasswordAsync("geovanealu@gmail.com", "Geovane21");

            return builder.Build();
        }
    }
}
