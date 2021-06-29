using System;
using System.Collections.Generic;

namespace lume.Domain
{
    public class PortaleLume
    {
        public Utente utenteCorrente { set; get; }

        public Lume lume { set; get; }

        private static PortaleLume portale;

        public static PortaleLume getIstance()
        {
            if (portale == null)
            {
                portale = new PortaleLume();
            }
            return portale;
        }

        private PortaleLume()
        {
            lume = Lume.getIstance();
        }


        public void nuovaRichiesta(string titolo ,string descrizione, int numeroPartecipanti)
        {
            Richiesta r = new Richiesta()
            {
                creatore = utenteCorrente,
                titolo = titolo,
                descrizione = descrizione,
                numeroPartecipanti = numeroPartecipanti
            };

            utenteCorrente.addRisciesta(r);
            lume.addRichiesta(r);
        }

        public void partecipaARichiesta(long richiestaId)
        {
            Richiesta r = lume.getRichiestaById(richiestaId);

            if(r.numeroPartecipanti <= r.getNumeroCandidati())
            {
                r.addCandidato(utenteCorrente);
            }
        }

        internal List<Richiesta> getAllRichieste()
        {
            return lume.richiesteList;
        }
    }
}
