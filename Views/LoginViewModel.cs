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
using GNAutoRota.Auth;
using GNAutoRota.Classes;
using Newtonsoft.Json;

namespace GNAutoRota.Views
{
    internal class LoginViewModel: INotifyPropertyChanged
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;

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

        public LoginViewModel(INavigation navigation, FirebaseAuthClient firebaseAuthClient) 
        {
            this._navigation = navigation;
            OnLoginbtn = new Command(OnLoginbtnTappedAsync);
            _firebaseAuthClient = firebaseAuthClient;
        }


        private async void OnLoginbtnTappedAsync(object obj)
        {
            try
            {
                await FirebaseServices.Login(Email, Password, this._navigation);
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
                            await App.Current.MainPage.DisplayAlert("Alerta", "Email inválido", "Ok");
                            break;
                        }
                   case "missing_password":
                        {
                            await App.Current.MainPage.DisplayAlert("Alerta", "campo senha está em branco", "Ok");
                            break;
                        }
                    case "missing_email":
                        {
                            await App.Current.MainPage.DisplayAlert("Alerta", "campo email está em branco", "Ok");
                            break;
                        }
                   case "invalid_login_credentials":
                        {
                            await App.Current.MainPage.DisplayAlert("Alerta", "Email ou senha incorretos", "Ok");
                            break;
                        }
                }
            }
            
        }
    }
}
