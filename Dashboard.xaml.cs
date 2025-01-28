
using Firebase.Auth;
using Newtonsoft.Json;

namespace GNAutoRota
{
	public partial class Dashboard : ContentPage
	{
        private readonly FirebaseAuthClient _firebaseAuthClient;

        public Dashboard(FirebaseAuthClient firebaseAuthClient)
		{
            _firebaseAuthClient = firebaseAuthClient;
            InitializeComponent();
            GetProfileInfo();
			
		}

		private void GetProfileInfo()
		{
			UserEmail.Text = _firebaseAuthClient.User.Info.Email;
        }
	}
}