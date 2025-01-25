
using Firebase.Auth;
using Newtonsoft.Json;

namespace GNAutoRota
{
	public partial class Dashboard : ContentPage
	{
		public Dashboard()
		{
			InitializeComponent();
			GetProfileInfo();
		}

		private void GetProfileInfo()
		{
			var userInfo = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
			UserEmail.Text = userInfo.User.Email;
		}
	}
}