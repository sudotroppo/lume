using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using lume.CustomObj;
using lume.Domain;
using lume.Pages;
using lume.Templates;
using lume.Utility;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Forms.Internals.Profile;

namespace lume.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class HomeViewModel : RichiestaPopUpViewModel
    {
        public ICommand RefreshPage => new Command(async () =>
        {
            Debug.WriteLine($"{IsRefreshing}");

            await Task.Run(() => Posts = new ObservableCollection<Richiesta>(DataAccess.GetAllRichieste()));

            IsRefreshing = false;
            Debug.WriteLine($"{IsRefreshing}");
        });



        public HomeViewModel()
        {
            Task.Run(() =>
              {
                  Posts = new ObservableCollection<Richiesta>(DataAccess.GetAllRichieste());

              });

        }

    }
}
