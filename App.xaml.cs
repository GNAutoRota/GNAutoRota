using Firebase.Auth;
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

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
