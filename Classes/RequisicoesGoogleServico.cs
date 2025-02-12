using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth.Requests;
using Newtonsoft.Json;

namespace GNAutoRota.Classes
{
    public class RequisicoesGoogleServico
    {
        private HttpClient _httpClient;

        public RequisicoesGoogleServico(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetNovoIdTokenAsync()
        {
            //isso ainda não vai funcionar pq ainda não estou resgantando o RefreshToken
            //tem que fazer um jeito de colocar essa ApiKey em um documento que não suba para o git
            //o google-services.json pode ser usado pra isso, mas tem que colocar ele no git ignore
            var url = $"https://securetoken.googleapis.com/v1/token?key={AppSettings.GetApiKey("FireBase")}";
            var payload = new
            {
                grant_type = "refresh_token",
                refresh_token = Preferences.Get("REFRESH_TOKEN",null) 
            };

            return await _httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

        }
    }
}
