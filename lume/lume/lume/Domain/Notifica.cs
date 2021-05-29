using System;
using lume.Domain;

namespace lume.Domain
{
    public class Notifica
    {
        private long id { get; set; }

        private string titolo { get; set; }

        private string descrizione { get; set; }

        private Richiesta richiesta { get; set; }

        private Utente utente { get; set; }

    }
}
