using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Auth.Providers;

namespace GNAutoRota.Auth
{
    public class FireBaseAuthClientInjecao
    {
        public FirebaseAuthClient ExecutaInjecaoServico()
        {
            return new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyAZ_nfIgxri-xNGEM6tXQVAYX6lfX_7PTY",
                AuthDomain = "gnautorota.firebaseapp.com",
                
                Providers = [new EmailProvider()]
            });
        }
    }
}
