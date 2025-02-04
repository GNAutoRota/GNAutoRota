using Firebase.Auth;
using Firebase.Auth.Repository;
using Firebase.Auth.Requests;
using FirebaseAdmin.Auth;
using GNAutoRota.Auth;
using GNAutoRota.Views;
using Plugin.FirebaseAuth;

namespace GNAutoRota
{
    public partial class App : Application
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;
        private IFirebaseAuth _firebaseAuth;

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
            //FirebaseServices.RestauraSessao();
            /*FirebaseServices.RestauraSessao().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Trate erros, se houver
                    Console.WriteLine($"Erro ao restaurar a sessão: {task.Exception}");
                }
            }); */
        }

        protected override void OnResume()
        {
            base.OnResume();
            // Lógica para quando o app volta para o primeiro plano
            FirebaseServices.RestauraSessao();
        }

    }
}
