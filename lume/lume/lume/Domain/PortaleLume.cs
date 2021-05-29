using System;
namespace lume.Domain
{
    public class PortaleLume
    {
        private Utente utenteCorrente;

        private Lume lume;

        public PortaleLume()
        {
        }

        public Utente GetUtenteCorrente()
        {
            return utenteCorrente;
        }

        public void SetUtenteCorrente(Utente utente)
        {
            utenteCorrente = utente;
        }

        public Lume GetLume()
        {
            return lume;
        }

        public void SetLume(Lume lume)
        {
            this.lume = lume;
        }
    }
}
