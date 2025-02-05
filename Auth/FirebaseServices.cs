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
using Google.Apis.Http;
using System.Net.Http;
using GNAutoRota.Views;
using GNAutoRota.ViewPrincipal;

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
            
            string idToken = await _firebaseAuth.Instance.CurrentUser.GetIdTokenAsync(true);
            Preferences.Set("FIREBASE_IDTOKEN", idToken);
            Preferences.Set("FIREBASE_UID", _firebaseAuth.Instance.CurrentUser.Uid);
            //await _firebaseAuth.Instance.SignInWithCustomTokenAsync(idTokenTeste);

            await navigation.PushAsync(new DashBoard(_firebaseAuthClient, _firebaseAuth));

        }
        public static async Task RestauraSessao()
        {
            var idToken = Preferences.Get("FIREBASE_IDTOKEN", null);
            var uid = Preferences.Get("FIREBASE_UID", null);

            //O usuário já está logado
            if (_firebaseAuth.Instance.CurrentUser is not null)
            {
                return;
            }

            //Sem idToken também não consigo restaurar sessão
            if (idToken is null)
            {    
                return;
            }

            //Recupera o IdToken
            try
            {
                var tokenDecodificado = await DecodeIdTokenAsync(idToken);
                if ((tokenDecodificado is not null) || (tokenDecodificado.Uid == uid))
                {
                    //não sei se essa é a forma correta de refazer o login
                    _firebaseAuth.Instance.SignInWithCustomTokenAsync(idToken);

                    await _navigation.PushAsync(new DashBoard(_firebaseAuthClient, _firebaseAuth));

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
            /* Falta implementar 
            var response = RequisicoesGoogleServico(_httpClient);

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
            */
            return null;
        }


    }
}
