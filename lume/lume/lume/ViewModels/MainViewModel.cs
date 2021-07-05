using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using lume.Domain;
using Xamarin.Forms;
using lume.Utility;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lume.ViewModels
{
    
    public class MainViewModel : BaseViewModel
    {
        Utente _CurrentUser = App.utenteCorrente;

        public ICommand SendRequest => new Command<string>((url) => Console.WriteLine(url));

        public Utente CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;
                OnPropertyChanged();
            }
        }

        public void SetUtente(Utente utente)
        {
            CurrentUser = utente;
        }

        public MainViewModel()
        {

        }
    }
}
