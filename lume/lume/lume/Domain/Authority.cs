using System.Text.Json.Serialization;

namespace lume.Domain
{
    public class Authority
    {
        [JsonPropertyName("authority")]
        public string authority { set; get; }
    }
}