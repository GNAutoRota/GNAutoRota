using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Auth.Providers;
using GNAutoRota.Classes;

namespace GNAutoRota.Auth
{
    public class FireBaseAuthClientInjecao
    {
        public FirebaseAuthClient ExecutaInjecaoServico()
        {
            return new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = AppSettings.GetApiKey("FireBase"),
                AuthDomain = "gnautorota.firebaseapp.com",

                Providers = [new EmailProvider()]
            });
        }
    }
}
