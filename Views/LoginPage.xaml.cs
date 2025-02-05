using Firebase.Auth;
using GNAutoRota.Models;
using Plugin.FirebaseAuth;
using System;
using System.Security.Cryptography.X509Certificates;

namespace GNAutoRota.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(FirebaseAuthClient firebaseAuthClient, IFirebaseAuth firebaseAuth)
	{
	
		InitializeComponent();
		BindingContext = new LoginViewModel(Navigation, firebaseAuthClient);
	}

    private void OnTogglePasswordClicked(object Sender, EventArgs e)
    {
        passwordEntry.IsPassword = !passwordEntry.IsPassword;

        var imageButton = (ImageButton)Sender;
        imageButton.Source = passwordEntry.IsPassword ? "view.png" : "hide.png";
    }

}