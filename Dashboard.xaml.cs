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
			//UserEmail.Text = _firebaseAuthClient.User.Info.Email;
        }
	}
}