using System;
using lume.Domain;

namespace lume.Domain
{
    public class Notifica
    {
        public long id { get; set; }

        public string titolo { get; set; }

        public string descrizione { get; set; }

        public Richiesta richiesta { get; set; }

        public Utente utente { get; set; }

    }
}
