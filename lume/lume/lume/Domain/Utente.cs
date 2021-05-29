using System;
using System.Collections.Generic;

namespace lume.Domain
{
    public class Utente
    {
        public long id { get; set; }

        public string nome { get; set; }

        public string cognome { get; set; }

        public string email { get; set; }

        public string telefono { get; set; }

        public Uri immagine { get; set; }

        public List<Notifica> notifiche { get; set; }

        public List<Richiesta> richieste { get; set; }


        public override string ToString()
        {
            return nome + " " + cognome;
        }
    }
}
