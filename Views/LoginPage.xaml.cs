using Firebase.Auth;
using Plugin.FirebaseAuth;
using System.Security.Cryptography.X509Certificates;

namespace GNAutoRota.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(FirebaseAuthClient firebaseAuthClient, IFirebaseAuth firebaseAuth)
	{
	
		InitializeComponent();
		BindingContext = new LoginViewModel(Navigation, firebaseAuthClient);
	}
}