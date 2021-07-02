using System;
using System.Collections.Generic;
using lume.Domain;
using lume.Utility;

namespace lume.ViewModels
{
    public class NotificationViewModel : BaseViewModel
    {

        List<Notifica> _notifiche;

        public List<Notifica> Notifiche
        {
            get { return _notifiche; }
            set
            {
                _notifiche = value;
                OnPropertyChanged();
            }
        }

        public NotificationViewModel(long utente_id)
        {
            if(Notifiche == null || Notifiche.Count == 0)
            {
                Notifiche = DataAccess.GetNotificheByUtente(utente_id);

            }
        }
    }
}
