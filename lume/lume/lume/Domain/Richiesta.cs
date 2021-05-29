using System;
using System.Collections.Generic;

namespace lume.Domain
{
    public class Richiesta
    {
        private long id { set; get; }

        private Utente creatore { set; get; }

        private int numeroPartecipanti { set; get; }

        private string titolo { set; get; }

        private string descrizione { set; get; }

        //private List<Uri> immagini { set; get; }

        private List<Utente> candidati { set; get; }


        public Richiesta()
        {
        }
    }
}
