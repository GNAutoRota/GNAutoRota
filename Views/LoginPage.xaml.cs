using System.Security.Cryptography.X509Certificates;
using Plugin.Firebase.Auth;

namespace GNAutoRota.Views;

public partial class LoginPage : ContentPage
{
    private IFirebaseAuth _firebaseAuth;

    public LoginPage(IFirebaseAuth firebaseAuth)
	{
		_firebaseAuth = firebaseAuth;
        InitializeComponent();
		BindingContext = new LoginViewModel(Navigation, _firebaseAuth);
	}
}