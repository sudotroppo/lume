using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using lume.Domain;
using Xamarin.Forms;

namespace lume.ViewModels
{
    
    public class MainViewModel : BaseViewModel
    {

        protected PortaleLume portaleLume = PortaleLume.getIstance();

        Utente _CurrentUser;

        public Utente CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            _CurrentUser = new Utente()
            {
                email = "domenicobini@pazzo.sgravato.it",
                nome = "Domenico",
                cognome = "Bini",
                telefono = "3337028903",
                immagine = new Uri("https://www.bellacanzone.it/wp-content/uploads/2019/12/Domenico-Bini-840x420.jpg"),
            };

            portaleLume.utenteCorrente = CurrentUser;

            

        }
    }
}
