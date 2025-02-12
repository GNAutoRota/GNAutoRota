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

namespace GNAutoRota.Models
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;

        private INavigation _navigation;
        private string email;
        private string password;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string Password
        {
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
            _navigation = navigation;
            OnLoginbtn = new Command(OnLoginbtnTappedAsync);
            _firebaseAuthClient = firebaseAuthClient;
        }


        private async void OnLoginbtnTappedAsync(object obj)
        {
            try
            {
                await FirebaseServices.Login(Email, Password, _navigation);
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case Constantes.emailFormatoInvalido:
                        {
                            await Application.Current.MainPage.DisplayAlert("Alerta", "Email em formato inválido", "Ok");
                            break;
                        }
                    case Constantes.emailOuSenhaVazio:
                        {
                            await Application.Current.MainPage.DisplayAlert("Alerta", "Email ou senha vazios", "Ok");
                            break;
                        }
                    case Constantes.emailOuSenhaIncorretos:
                        {
                            await Application.Current.MainPage.DisplayAlert("Alerta", "Email ou senha incorretos", "Ok");
                            break;
                        }
                }
            }

        }
    }
}
