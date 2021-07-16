using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json.Serialization;
using lume.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lume.Domain
{
    public class Richiesta : INotifyPropertyChanged
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

        [JsonPropertyName("immagine")]
        public string immagine { set; get; }

        [JsonPropertyName("candidati")]
        public ObservableCollection<Utente> candidati { set; get; }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                var result = candidati.ToArray().ToList().Find((u) => u.id == App.utente.id) != null;
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
                if(descrizione.Length > 27)
                {
                    return StringExt.Truncate(descrizione, 27) + "...";

                }

                return descrizione;
            }
        }

        [JsonIgnore]
        public ImageSource sorgenteImmagine
        {
            get
            {
                if (immagine == null || "".Equals(immagine))
                {
                    return ImageSource.FromFile("uploadImage.png");

                }

                return ImageSource.FromUri(new Uri(immagine));
            }
        }

        public Richiesta()
        {
            candidati = new ObservableCollection<Utente>();
        }

        public void removeCandidato(Utente utente)
        {
            candidati.Remove(utente);
        }

        public void addCandidato(Utente candidato)
        {
            candidati.Add(candidato);
        }
    }
}
