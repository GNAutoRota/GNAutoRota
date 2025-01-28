using Firebase.Auth;
using Firebase.Auth.Requests;
using GNAutoRota.Views;

namespace GNAutoRota
{
    public partial class App : Application
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;
        public App(FirebaseAuthClient firebaseAuthClient)
        {
            InitializeComponent();
            _firebaseAuthClient = firebaseAuthClient;
            var user = _firebaseAuthClient.User;

            if (user is null)
            {
                MainPage = new NavigationPage(new LoginPage(_firebaseAuthClient));
            }
            else
            {
                MainPage = new NavigationPage(new Dashboard(_firebaseAuthClient));
            }


        }
    }
}
