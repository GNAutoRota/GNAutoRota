using FirebaseAdmin;
using GNAutoRota.Auth;
using GNAutoRota.Views;
using Plugin.Firebase.Auth;

namespace GNAutoRota
{
    public partial class App : Application
    {
        private IFirebaseAuth _firebaseAuth;

        public App(IFirebaseAuth firebaseAuth)
        {
            
            InitializeComponent();
            _firebaseAuth = firebaseAuth;
            MainPage = new NavigationPage(new LoginPage(_firebaseAuth));

        }

        protected override void OnStart()
        {
            base.OnStart();
            // Lógica para quando o app Iniciar
            FirebaseServices.InicializarFirebase(_firebaseAuth);
            if (_firebaseAuth.CurrentUser is null)
                MainPage = new NavigationPage(new LoginPage(_firebaseAuth));
            else
                MainPage = new NavigationPage(new Dashboard());
        }

        protected override void OnResume()
        {
            base.OnResume();
            // Lógica para quando o app volta para o primeiro plano
            //if (_firebaseAuth.CurrentUser is null)
            FirebaseServices.RestauraSessao();
            

        }

    }
}
