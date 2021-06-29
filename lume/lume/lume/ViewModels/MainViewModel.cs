using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using lume.Domain;
using Xamarin.Forms;

namespace lume.ViewModels
{
    
    class MainViewModel : BaseViewModel
    {
        List<Richiesta> _Posts;

        List<Notifica> _notifiche;

        public List<Richiesta> Posts
        {
            get { return _Posts; }

            set
            {
                _Posts = value;
                OnPropertyChanged();
            }
        }

        public List<Notifica> Notifiche
        {
            get { return _notifiche; }
            set
            {
                _notifiche = value;
                OnPropertyChanged();
            }
        }

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
            _CurrentUser = new Utente
            {
                email = "domenicobini@pazzo.sgravato.it",
                nome = "Domenico",
                cognome = "Bini",
                telefono = "3337028903",
                immagine = new Uri("https://www.bellacanzone.it/wp-content/uploads/2019/12/Domenico-Bini-840x420.jpg"),
            };

            _notifiche = new List<Notifica>
            {
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = CurrentUser,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = CurrentUser,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = CurrentUser,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = CurrentUser,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = CurrentUser,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = CurrentUser,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = CurrentUser,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = CurrentUser,
                },
                new Notifica()
                {
                    descrizione = "Prova notifica",
                    utente = CurrentUser,
                },

            };

            _Posts = new List<Richiesta>
            {
               new Richiesta()
               {
                   creatore = CurrentUser,
                   titolo = "Adjosbnow",
                   descrizione = "dkjcbwivbiwirbviwer webf iwrb guerig iuer " +
                   "wrg whbrg iebreir ve erg ereoriervierververbsaethdgfhj u tm t rtj ysrt hsr" +
                   " rtsrtjrstj ajt rjr stj sr tjdtjysrjetrr tr rthgretgergdgrgegegege e",
                   numeroPartecipanti = 10,
                   //Immagini =
                   //{
                   //    new Uri( "https://www.ilgiardinodilori.it/images/articoli/fiori-recisi.jpg"),
                   //    new Uri( "https://www.ilgiardinodilori.it/images/articoli/fiori-recisi.jpg"),
                   //}
               }

            };
        }
    }
}
