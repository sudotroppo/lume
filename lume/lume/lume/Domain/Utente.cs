using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xamarin.Forms;

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

        public override string ToString()
        {
            return nome + " " + cognome;
        }

        public override bool Equals(object obj)
        {
            if(obj == null || !typeof(Utente).Equals(obj))
            {
                return false;
            }

            Utente that = obj as Utente;

            return this.id == that.id;
        }

    }
}
