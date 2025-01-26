using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Firebase.Auth;
using GNAutoRota.Classes;
using Newtonsoft.Json;

namespace GNAutoRota.Views
{
    internal class LoginViewModel: INotifyPropertyChanged
    {
        public string webapikey = "AIzaSyAZ_nfIgxri-xNGEM6tXQVAYX6lfX_7PTY";

        private INavigation _navigation;
        private string email;
        private string password;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Email { get => email; 
            set{
                email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string Password { 
            get => password; set 
            { 
                password = value;
                RaisePropertyChanged("Password");
            } 
        }

        public Command OnLoginbtn { get; }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public LoginViewModel(INavigation navigation) 
        {
            this._navigation = navigation;
            OnLoginbtn = new Command(OnLoginbtnTappedAsync);
        }


        private async void OnLoginbtnTappedAsync(object obj)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webapikey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email,Password);
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("FreshFirebaseToken", serializedContent);
                

                await this._navigation.PushAsync(new Dashboard());
            }
            catch (Exception ex)
            {
                
                string jSonResponse = JsonConvert.SerializeObject(ex.Message);
                Match match = Regex.Match(jSonResponse, @"Response:\s*(\{.*\})", RegexOptions.Singleline);

                string response;
                if (match.Success)
                {
                    response = match.Groups[1].Value;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", ex.Message, "Ok");
                    throw;
                }

                string jsonLimpo = response.Replace("\\n", "").Replace("\\\"", "\"");
                jsonLimpo = Regex.Replace(jsonLimpo, @"\s+", "");

                var jSonObject = JsonConvert.DeserializeObject<ApiResponse>(jsonLimpo);
                
                switch (jSonObject.Error.mensagem.ToLower())
                {
                   case "invalid_email":
                        {
                            await App.Current.MainPage.DisplayAlert("Alerta", "email inválido", "Ok");
                            break;
                        }

                }
            }
            
        }
    }
}
