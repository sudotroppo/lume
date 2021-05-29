using System.Collections.Generic;

namespace lume.Domain
{

    public class Lume
    {
        private List<Richiesta> richieste;

        private List<Utente> utenti;

        public Lume()
        {

        }

        public List<Richiesta> getRichieste()
        {
            return richieste;
        }

        public void SetRichieste(List<Richiesta> richieste)
        {
            this.richieste = richieste;
        }

        public List<Utente> GetUtenti()
        {
            return utenti;
        }

        public void SetUtenti(List<Utente> utenti)
        {
            this.utenti = utenti;
        }
    }
}