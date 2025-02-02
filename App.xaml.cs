using GNAutoRota.Auth;
using GNAutoRota.Views;

namespace GNAutoRota
{
    public partial class App : Application
    {

        public App()
        {
            
            InitializeComponent();
            
            MainPage = new NavigationPage(new LoginPage());
            


        }

        protected override void OnStart()
        {
            base.OnStart();
            // Lógica para quando o app Iniciar
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
