using Firebase.Auth;
using Firebase.Auth.Repository;
using Firebase.Auth.Requests;
using FirebaseAdmin.Auth;
using GNAutoRota.Auth;
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
            
            MainPage = new NavigationPage(new LoginPage(_firebaseAuthClient));
            


        }

        protected override void OnStart()
        {
            base.OnStart();
            // Lógica para quando o app Iniciar
            FirebaseServices.InicializarFirebase(_firebaseAuthClient);
            FirebaseServices.RestauraSessao();
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
