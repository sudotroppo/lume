using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using lume.Utility;
using Xamarin.Forms;

namespace lume.Domain
{
    public class Utente
    {

        [JsonPropertyName("id")]
        public long id { get; set; }

        [JsonPropertyName("nome")]
        public string nome { get; set; }

        [JsonPropertyName("citta")]
        public string citta { get; set; }

        [JsonPropertyName("cognome")]
        public string cognome { get; set; }

        [JsonPropertyName("email")]
        public string email { get; set; }

        [JsonPropertyName("telefono")]
        public string telefono { get; set; }

        [JsonPropertyName("password")]
        public string password { get; set; }

        [JsonPropertyName("immagine")]
        public string immagine { get; set; }

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

        [JsonIgnore]
        public ImageSource sorgenteImmagine
        {
            get
            {
                if(immagine == null || immagine.Equals(""))
                {
                    return ImageSource.FromFile("user.png");
                }

                return ImageSource.FromUri(new Uri(immagine));
            }
        }

        [JsonIgnore]
        public string fullName
        {
            get
            {
                
                return $"{nome} {cognome}";
            }
        }


        [JsonIgnore]
        public string shortName
        {
            get
            {
                if(nome.Length > 25)
                {
                    return StringExt.Truncate($"{nome} {cognome}", 25) + "...";

                }
                return nome;
            }
        }

        public override string ToString()
        {
            return nome + " " + cognome;
        }

        public Utente Copy()
        {
            return (Utente)MemberwiseClone();
        }
    }
}
