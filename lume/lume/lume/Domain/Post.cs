using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace lume.Domain
{
    public class Richiesta
    {
        public Utente Creatore { get; set; }

        public int Partecipanti { get; set; }

        public string Titolo { set; get; }

        public string Descrizione
        {
            set;
            get;
        }

        public string Anteprima;

        public List<Uri> Immagini { set; get; }


        public Richiesta()
        {
            Immagini = new List<Uri>();
        }

    }
}
