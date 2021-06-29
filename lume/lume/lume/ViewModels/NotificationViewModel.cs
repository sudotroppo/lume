using System;
using System.Collections.Generic;
using lume.Domain;

namespace lume.ViewModels
{
    public class NotificationViewModel : BaseViewModel
    {

        List<Notifica> _notifiche;

        protected PortaleLume portaleLume = PortaleLume.getIstance();

        public List<Notifica> Notifiche
        {
            get { return _notifiche; }
            set
            {
                _notifiche = value;
                OnPropertyChanged();
            }
        }

        public NotificationViewModel()
        {

            _notifiche = new List<Notifica>
            {
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = portaleLume.utenteCorrente,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = portaleLume.utenteCorrente,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = portaleLume.utenteCorrente,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = portaleLume.utenteCorrente,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = portaleLume.utenteCorrente,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = portaleLume.utenteCorrente,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = portaleLume.utenteCorrente,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = portaleLume.utenteCorrente,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = portaleLume.utenteCorrente,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = portaleLume.utenteCorrente,
                },

            };
        }
    }
}
