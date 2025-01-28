
using Firebase.Auth;
using Newtonsoft.Json;

namespace GNAutoRota
{
	public partial class Dashboard : ContentPage
	{
        private readonly UserInfo _userInfo;

        public Dashboard(UserInfo userInfo)
		{
            _userInfo = userInfo;
            InitializeComponent();
            GetProfileInfo();
			
		}

		private void GetProfileInfo()
		{
			var userInfo = JsonConvert.DeserializeObject<Firebase.Auth.UserInfo>(Preferences.Get("FreshFirebaseToken", ""));
            UserEmail.Text = userInfo.Email;
            UserEmail.Text = _userInfo.Email;
			
        }
	}
}