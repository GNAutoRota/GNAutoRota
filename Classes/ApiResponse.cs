using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GNAutoRota.Classes
{
    internal class ApiResponse
    {
        [JsonProperty("error")]
        public ErrorResponse Error { get; set; }
        
    }

    public class ErrorResponse
    {
        [JsonProperty("code")]
        public int codigo { get; set; }
        [JsonProperty("message")]
        public string mensagem { get; set; }
    }
}
