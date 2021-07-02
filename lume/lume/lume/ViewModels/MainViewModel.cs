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
        public ICommand SendRequest => new Command<string>((url) => Console.WriteLine(url));

        private PortaleLume portale = PortaleLume.getIstance();

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

        public void SetUtente(string email)
        {
            portale.utenteCorrente = DataAccess.GetUtenteByEmail(email) ?? CurrentUser;
            CurrentUser = portale.utenteCorrente;
        }

        public MainViewModel()
        {


        }
    }
}
