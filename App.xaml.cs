using System.Net.Http;
using Firebase.Auth;
using Firebase.Auth.Repository;
using Firebase.Auth.Requests;
using FirebaseAdmin.Auth;
using GNAutoRota.Auth;
using GNAutoRota.ViewPrincipal;
using GNAutoRota.Views;
using Microsoft.Maui.Storage;
using Plugin.FirebaseAuth;

namespace GNAutoRota
{
    public partial class App : Application
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;
        private IFirebaseAuth _firebaseAuth;
        private INavigation _navigation;

        public App(FirebaseAuthClient firebaseAuthClient, IFirebaseAuth firebaseAuth)
        {
            
            InitializeComponent();
            _firebaseAuthClient = firebaseAuthClient;
            _firebaseAuth = firebaseAuth;

            MainPage = new NavigationPage(new LoginPage(_firebaseAuthClient, _firebaseAuth));
        }

        protected override void OnStart()
        {
            base.OnStart();
            // Lógica para quando o app Iniciar
            FirebaseServices.InicializarFirebase(_firebaseAuthClient, _firebaseAuth);
            
            if (_firebaseAuth.Instance.CurrentUser is not null)
            {
                MainPage = new NavigationPage(new DashBoard(_firebaseAuthClient, _firebaseAuth));
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            // Lógica para quando o app volta para o primeiro plano
            if (_firebaseAuth.Instance.CurrentUser is null)
            {
                //FirebaseServices.RestauraSessao();
                MainPage = new NavigationPage(new LoginPage(_firebaseAuthClient, _firebaseAuth));
                return;
            }

            

        }

    }
}
