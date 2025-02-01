using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using GNAutoRota.Classes;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace GNAutoRota.Auth
{
    public class FirebaseServices
    {
        private static FirebaseAuthClient _firebaseAuthClient;
        private static NavigationPage _navigation;

        public static void InicializarFirebase(FirebaseAuthClient firebaseAuthClient) 
        {
            _firebaseAuthClient = firebaseAuthClient;
            _navigation = new NavigationPage();
        }

        private void OnAuthStateChanged(object? sender, UserEventArgs e)
        {
            if (e.User != null)
            {
                Console.WriteLine($"Usuário autenticado: {e.User.Info.DisplayName}");
            }
            else
            {
                Console.WriteLine("Usuário deslogado.");
            }
        }

        public static async Task Login(string email,  string password, INavigation navigation)
        {
            await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);
            // Salvar o token de autenticação localmente para manter a sessão
            Preferences.Set("FIREBASE_REFRESHTOKEN", _firebaseAuthClient.User.Credential.RefreshToken);
            Preferences.Set("FIREBASE_UID", _firebaseAuthClient.User.Info.Uid);

            await navigation.PushAsync(new Dashboard(_firebaseAuthClient));

        }
        public async Task RestauraSessao()
        {
            var refreshToken = Preferences.Get("FIREBASE_REFRESHTOKEN", null);
            var uid = Preferences.Get("FIREBASE_UID", null);

            if (string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(uid))
            {
                Console.WriteLine("Nenhum token ou UID encontrado. O usuário precisa fazer login.");
                return;
            }

            //Recuperar o IdToken
            try
            {
                string novoIdToken = await RefreshIdTokenAsync(refreshToken);
                //var userInfo = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("FIREBASE_IDTOKEN", ""));

                if (!string.IsNullOrEmpty(novoIdToken)) 
                {
                    var tokenDecodificado = await DecodeIdTokenAsync(novoIdToken);

                    if ((tokenDecodificado is not null) || (tokenDecodificado.Uid == uid))
                    {

                        if (_firebaseAuthClient.User.Info.Uid is null)
                        {
                            _firebaseAuthClient.User.Info.Uid = tokenDecodificado.Uid;
                        }

                        await _navigation.PushAsync(new Dashboard(_firebaseAuthClient));
                        
                    }
                }

            }
            catch(Exception ex) 
            {
                string erro = ex.Message;

            }
        }

        private async Task<FirebaseToken> DecodeIdTokenAsync(string idToken)
        {
            try
            {
                //decodificando o token 
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(idToken);

                // Extraindo o UID do token
                var uid = token.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;

                if (!string.IsNullOrEmpty(uid))
                {
                    return new FirebaseToken { Uid = uid };
                }

            }
            catch (Exception ex)
            {
                throw new NotImplementedException($"Erro ao decodificar o token: {ex.Message}");
            }

            return null;
        }

        private class FirebaseToken
        {
            public string Uid { get; set; }
        }

        private async Task<string> RefreshIdTokenAsync(string refreshToken)
        {
            var url = $"https://securetoken.googleapis.com/v1/token?key={""}";
            var payload = new
            {
                grant_type = "refresh_token",
                refresh_token = refreshToken
            };

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<FbTokenResponse>(responseData);
                    return tokenResponse.IdToken;
                }
                else
                {
                    Console.WriteLine($"Erro ao renovar o token: {response.StatusCode}");
                    return null;
                }
            }
        }


    }
}
