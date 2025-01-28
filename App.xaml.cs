using Firebase.Auth;
using Firebase.Auth.Requests;
using GNAutoRota.Views;

namespace GNAutoRota
{
    public partial class App : Application
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;
        private readonly UserInfo _userInfo;
        public App(FirebaseAuthClient firebaseAuthClient, UserInfo userInfo)
        {
            InitializeComponent();
            _firebaseAuthClient = firebaseAuthClient;
            _userInfo = userInfo;
            var user = _firebaseAuthClient.User;

            if (user.Info?.Email == null)
            {
                MainPage = new NavigationPage(new LoginPage(_firebaseAuthClient));
            }
            else
            {
                MainPage = new NavigationPage(new Dashboard(_userInfo));
            }


        }
    }
}
