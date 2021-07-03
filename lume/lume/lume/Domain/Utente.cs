using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace lume.Domain
{
    public class Utente
    {
        [JsonPropertyName("id")]
        public long id { get; set; }

        [JsonPropertyName("nome")]
        public string nome { get; set; }

        [JsonPropertyName("cognome")]
        public string cognome { get; set; }

        [JsonPropertyName("email")]
        public string email { get; set; }

        [JsonPropertyName("telefono")]
        public string telefono { get; set; }

        [JsonPropertyName("immagine")]
        public string immagine { get; set; }

        public Uri uriImmagine
        {
            get { return new Uri(immagine); }
        }

        [JsonPropertyName("notifiche")]
        public List<Notifica> notifiche { get; set; }

        [JsonPropertyName("richiestePartecipate")]
        public List<Richiesta> richiestePartecipate { set; get; }

        [JsonPropertyName("richieste")]
        public List<Richiesta> richieste { get; set; }

        public Utente()
        {
            richieste = new List<Richiesta>();
            notifiche = new List<Notifica>();
        }

        public override string ToString()
        {
            return nome + " " + cognome;
        }

    }
}
