using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace lume.Domain
{
    public class TokenResponse
    {
        [JsonPropertyName("email")]
        public string email { set; get; }

        [JsonPropertyName("authorities")]
        public List<Authority> autorita { set; get; }

        [JsonPropertyName("token")]
        public string token { set; get; }


        public override string ToString()
        {
            return token;
        }
    }
}
