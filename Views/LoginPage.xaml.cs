using GNAutoRota.Views.Login;

namespace GNAutoRota.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel(Navigation);
	}

	private void VisualizaSenha(object Sender, EventArgs e)
	{
		//passwordEntry.IsPassword = !passwordEntry.IsPassword;
	}

	private void OnClickLogin(object Sender, EventArgs e)
	{
		/*if ((userEntry.Text is null) || (passwordEntry.Text is null))
		{
			DisplayAlert("Autenticação", "Usuário ou senha incorretos", "Ok");
			return;
		}

        var usuario = userEntry.Text;
		var senha = passwordEntry.Text;

		if ((usuario != "03331033278") || (senha != "Geovane21"))
		{
			DisplayAlert("Autenticação", "Usuário ou senha incorretos", "Ok");
			return;
		}

		DisplayAlert("Autenticação", "Logado com sucesso!", "Ok"); */

    }
}