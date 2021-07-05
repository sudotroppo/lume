using System;
using System.Text.Json.Serialization;

namespace lume.Domain
{
    public class TokenRequest
    {
        [JsonPropertyName("email")]
        public string email { set; get; }

        [JsonPropertyName("password")]
        public string password { set; get; }

        public TokenRequest()
        {
        }
    }
}
