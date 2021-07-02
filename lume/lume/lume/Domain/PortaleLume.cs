using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using lume.Utility;

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
                numeroPartecipanti = numeroPartecipanti,

            };

            DataAccess.NewRichiesta(r);
        }

        public List<Richiesta> getAllRichieste()
        {
            return DataAccess.GetAllRichieste();
        }
    }
}
