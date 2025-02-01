using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GNAutoRota.Classes
{
    public class FbTokenResponse
    {
        [JsonProperty("access_token")]
        public string AcessToken { get; set; }
        [JsonProperty("id_token")]
        public string IdToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("user_id")]
        public string Uid { get; set; }
        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

    }
}
