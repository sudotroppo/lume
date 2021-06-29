using System;
using System.Collections.Generic;

namespace lume.Domain
{
    public class Richiesta
    {
        public long id { set; get; }

        public Utente creatore { set; get; }

        public int numeroPartecipanti { set; get; }

        public string titolo { set; get; }

        public string descrizione { set; get; }

        //private List<Uri> immagini { set; get; }

        public List<Utente> candidati { set; get; }


        public Richiesta()
        {
            candidati = new List<Utente>();
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
