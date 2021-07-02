using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace lume.Domain
{
    public class Richiesta
    {
        [JsonPropertyName("id")]
        public long id { set; get; }

        [JsonPropertyName("creatore")]
        public Utente creatore { set; get; }

        [JsonPropertyName("numeroPartecipanti")]
        public int numeroPartecipanti { set; get; }

        [JsonPropertyName("titolo")]
        public string titolo { set; get; }

        [JsonPropertyName("descrizione")]
        public string descrizione { set; get; }

        [JsonPropertyName("dataCreazione")]
        public DateTime dataCreazione { set; get; }

        //private List<Uri> immagini { set; get; }

        [JsonPropertyName("candidati")]
        public List<Utente> candidati { set; get; }

        [JsonPropertyName("notifiche")]
        public List<Notifica> notifiche { set; get; }


        public Richiesta()
        {
            candidati = new List<Utente>();
            notifiche = new List<Notifica>();
        }

        public void addCandidato(Utente candidato)
        {
            candidati.Add(candidato);
        }

        public int getNumeroCandidati()
        {
            return candidati.ToArray().Length;
        }
    }
}
