using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using lume.Domain;
using lume.Utility;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace lume.ViewModels
{
    public class PartecipationViewModel : RichiestaPopUpViewModel
    {

        public PartecipationViewModel()
        {
            Task.Run(() =>
            {
                Debug.WriteLine("Le mie richieste");
                Posts = new ObservableCollection<Richiesta>(DataAccess.GetPartecipazioniUtente());
                
            });

        }



    }
}
