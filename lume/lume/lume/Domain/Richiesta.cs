using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json.Serialization;
using lume.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Linq;

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
        public List<string> immagini { set; get; }

        [JsonPropertyName("candidati")]
        public List<Utente> candidati { set; get; }


        [JsonIgnore]
        public bool isNonVuoto
        {
            get
            {
                return immagini.Count > 0;
            }
        }


        [JsonIgnore]
        public bool isTheOwner
        {
            get
            {
                var result = creatore.id == App.utente.id;
                Debug.WriteLine($"isTheOwner? {result}");
                return result;
            }
        }


        [JsonIgnore]
        public bool alreadyPicked
        {
            get
            {
                var result = candidati.Find((u) => u.id == App.utente.id) != null;
                Debug.WriteLine($"alreadyPicked? {result}");
                return result;
            }
        }

        public int numeroCandidati
        {
            get
            {
                return candidati.Count;
            }
        }

        [JsonIgnore]
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


        [JsonIgnore]
        public string shortDescrizione
        {
            get
            {
                if(descrizione.Length > 37)
                {
                    return StringExt.Truncate(descrizione, 37) + "...";

                }

                return descrizione;
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
