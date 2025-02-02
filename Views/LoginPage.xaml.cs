using System.Security.Cryptography.X509Certificates;

namespace GNAutoRota.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
	
		InitializeComponent();
		BindingContext = new LoginViewModel(Navigation);
	}
}