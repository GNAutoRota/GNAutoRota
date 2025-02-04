using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using GNAutoRota.Classes;
using Newtonsoft.Json;
using GNAutoRota;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Plugin.FirebaseAuth;
using FirebaseAdmin.Auth;

namespace GNAutoRota.Auth
{
    public class FirebaseServices
    {
        private static FirebaseAuthClient _firebaseAuthClient;
        private static NavigationPage _navigation;
        private static IFirebaseAuth _firebaseAuth;

        public static void InicializarFirebase(FirebaseAuthClient firebaseAuthClient, IFirebaseAuth firebaseAuth) 
        {
            _firebaseAuthClient = firebaseAuthClient;
            _firebaseAuth = firebaseAuth;
            _navigation = new NavigationPage();
        }

        public class FirebaseTokenUser
        {
            public string Uid { get; set; }
            public string Email { get; set; }
        }

        public static async Task Login(string email,  string password, INavigation navigation)
        {
            await _firebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

            //await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);
            // Salvar o token de autenticação localmente para manter a sessão
            string idTokenTeste = await _firebaseAuth.Instance.CurrentUser.GetIdTokenAsync(true);
            var idTokenTesteFalse = await _firebaseAuth.Instance.CurrentUser.GetIdTokenAsync(false);

            //_firebaseAuth.
            await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);
            await _firebaseAuth.Instance.SignInWithCustomTokenAsync(idTokenTeste);

            Preferences.Set("FIREBASE_REFRESHTOKEN", idTokenTeste);
            Preferences.Set("FIREBASE_REFRESHTOKEN", _firebaseAuthClient.User.Credential.RefreshToken);
            Preferences.Set("FIREBASE_UID", _firebaseAuthClient.User.Info.Uid);

            await navigation.PushAsync(new Dashboard(_firebaseAuthClient));

        }
        public static async Task RestauraSessao()
        {
            var refreshToken = Preferences.Get("FIREBASE_REFRESHTOKEN", null);
            var uid = Preferences.Get("FIREBASE_UID", null);

            if (string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(uid))
            {
                //Console.WriteLine("Nenhum token ou UID encontrado. O usuário precisa fazer login.");
                return;
            }

            //Recuperar o IdToken
            try
            {
                var novoIdToken = await RefreshIdTokenAsync(refreshToken);
                //var userInfo = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("FIREBASE_IDTOKEN", ""));

                if (!string.IsNullOrEmpty(novoIdToken)) 
                {
                    var tokenDecodificado = await DecodeIdTokenAsync(novoIdToken);

                    if ((tokenDecodificado is not null) || (tokenDecodificado.Uid == uid))
                    {
                        
                        _firebaseAuthClient.User.Info.Uid = tokenDecodificado.Uid;

                        await _navigation.PushAsync(new Dashboard(_firebaseAuthClient));
                        
                    }
                }

            }
            catch(Exception ex) 
            {
                string erro = ex.Message;

            }
        }

        private static async Task<FirebaseTokenUser> DecodeIdTokenAsync(string idToken)
        {
            try
            {
                //decodificando o token 
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(idToken);

                // Extraindo o UID do token
                var user = new FirebaseTokenUser
                {
                    Uid = token.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value,
                    Email = token.Claims.FirstOrDefault(c => c.Type == "email")?.Value
                };
                
                return user;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException($"Erro ao decodificar o token: {ex.Message}");
            }

            return null;
        }

        private static async Task<string> RefreshIdTokenAsync(string refreshToken)
        {

            var url = $"https://securetoken.googleapis.com/v1/token?key={"AIzaSyAZ_nfIgxri-xNGEM6tXQVAYX6lfX_7PTY"}";
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
