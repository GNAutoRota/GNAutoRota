using GNAutoRota.Views;

namespace GNAutoRota
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }
    }
}
