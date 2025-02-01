using Firebase.Auth;
using System.Security.Cryptography.X509Certificates;

namespace GNAutoRota.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(FirebaseAuthClient firebaseAuthClient)
	{
	
		InitializeComponent();
		BindingContext = new LoginViewModel(Navigation, firebaseAuthClient);
	}
}