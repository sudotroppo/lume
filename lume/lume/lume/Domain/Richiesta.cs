using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        [JsonPropertyName("immagini")]
        public ObservableCollection<string> immagini { set; get; }

        [JsonPropertyName("candidati")]
        public List<Utente> candidati { set; get; }

        public bool alreadyPicked
        {
            get
            {
                return candidati.Contains(App.utente);
            }
        }

        public int numeroCandidati
        {
            get
            {
                return candidati.Count;
            }
        }


        public string data
        {
            get
            {
                var diff = DateTime.Now.ToUniversalTime().Subtract(dataCreazione.ToUniversalTime());


                if (diff.TotalSeconds < 60)
                {
                    return diff.Seconds.ToString() + "s fa";
                }
                else if(diff.TotalMinutes < 60)
                {
                    return diff.Minutes.ToString() + "m fa";
                }
                else if (diff.TotalHours < 24)
                {
                    return diff.Hours.ToString() + "h fa";
                }
                else if (diff.TotalDays < 31)
                {
                    return dataCreazione.ToString("ddd dd MMM yyyy");
                }

                return dataCreazione.ToString("dd/MM/yyyy");
            }
        }


        public Richiesta()
        {
            candidati = new List<Utente>();
        }

        public void addCandidato(Utente candidato)
        {
            candidati.Add(candidato);
        }
    }
}
