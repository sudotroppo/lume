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

        public List<Richiesta> Posts
        {
            get { return _Posts; }
            set
            {
                _Posts = value;
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
                Indirizzo = "domenicobini@pazzo.sgravato.it",
                Nome = "Domenico",
                Cognome = "Bini",
                Immagine = new UriImageSource
                {
                    Uri = new Uri("https://www.bellacanzone.it/wp-content/uploads/2019/12/Domenico-Bini-840x420.jpg"),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(24,0,0),
                }
            };

            _Posts = new List<Richiesta>
            {
               new Richiesta()
               {
                   Creatore = CurrentUser,
                   Titolo = "Adjosbnow",
                   Descrizione = "dkjcbwivbiwirbviwer webf iwrb guerig iuer " +
                   "wrg whbrg iebreir ve erg ereoriervierververbsaethdgfhj u tm t rtj ysrt hsr" +
                   " rtsrtjrstj ajt rjr stj sr tjdtjysrjetrr tr rthgretgergdgrgegegege e",
                   Partecipanti = 10,
                   Immagini =
                   {
                       new Uri( "https://www.ilgiardinodilori.it/images/articoli/fiori-recisi.jpg"),
                       new Uri( "https://www.ilgiardinodilori.it/images/articoli/fiori-recisi.jpg"),
                   }
               },
               new Richiesta()
               {
                   Creatore = CurrentUser,
                   Titolo = "Mskndvp",
                   Descrizione = "dkjcbwivbiwirbviwer webf iwrb guerig iuer " +
                   "wrg whbrg iebreir ve erg ereoriervierververbsaethdgfhj u tm t rtj ysrt hsr" +
                   " rtsrtjrstj ajt rjr stj sr tjdtjysrjetrr tr rthgretgergdgrgegegege e",
                   Partecipanti = 1,
               },
               new Richiesta()
               {
                   Creatore = CurrentUser,
                   Titolo = "Mskndvp",
                   Descrizione = "dkjcbwivbiwirbviwer webf iwrb guerig iuer " +
                   " rtsrtjrstj ajt rjr stj sr tjdtjysrjetrr tr rthgretgergdgrgegegege e",
                   Partecipanti = 1,
                   Immagini =
                   {
                       new Uri( "https://www.ilgiardinodilori.it/images/articoli/fiori-recisi.jpg"),
                       new Uri( "https://www.ilgiardinodilori.it/images/articoli/fiori-recisi.jpg"),
                   }
               }

            };
        }
    }
}
