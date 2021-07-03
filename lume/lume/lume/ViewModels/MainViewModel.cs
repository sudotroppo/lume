using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using lume.Domain;
using Xamarin.Forms;
using lume.Utility;
using System.Windows.Input;

namespace lume.ViewModels
{
    
    public class MainViewModel : BaseViewModel
    {
        Utente _CurrentUser;


        public ICommand SendRequest => new Command<string>((url) => Console.WriteLine(url));


        public bool Load
        {
            set
            {
                Load = value;
                OnPropertyChanged();
            }

        }

        public void SetLoad(bool load)
        {
            Load = load;
        }

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
            CurrentUser = DataAccess.GetUtenteByEmail(email);
        }


        public MainViewModel()
        {

        }
    }
}
