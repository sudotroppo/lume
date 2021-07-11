using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using lume.Domain;
using Xamarin.Forms;
using lume.Utility;
using System.Threading.Tasks;
using System.Windows.Input;
using lume.Templates;
using System.IO;
using lume.CustomObj;
using FFImageLoading.Forms;
using Xamarin.Essentials;
using System.Diagnostics;

namespace lume.ViewModels
{
    
    public class MainViewModel : BaseViewModel
    {

        private Utente _CurrentUser = App.utente;

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



        public MainViewModel()
        {
            
        }



    }
}
