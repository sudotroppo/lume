using System;
using System.Text.Json.Serialization;
using lume.Domain;

namespace lume.Domain
{
    public class Notifica
    {

        [JsonPropertyName("id")]
        public long id { get; set; }


        [JsonPropertyName("titolo")]
        public string titolo { get; set; }


        [JsonPropertyName("descrizione")]
        public string descrizione { get; set; }


        [JsonPropertyName("richiesta")]
        public Richiesta richiesta { get; set; }


        [JsonPropertyName("utente")]
        public Utente utente { get; set; }

        [JsonPropertyName("soggetto")]
        public Utente soggetto { get; set; }
    }
}
