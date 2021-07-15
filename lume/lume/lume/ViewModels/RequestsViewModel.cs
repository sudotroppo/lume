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
using lume.CustomObj;
using lume.Pages;

namespace lume.ViewModels
{
    public class RequestsViewModel : RichiestaPopUpViewModel
    {

        public ICommand RichiediRimozioneCommand { get; private set; }
        public ICommand RemoveRichiestaCommand { get; private set; }
        public ICommand RefreshPage { get; private set; }

        public long IdRichiesta { private set; get; }

        public RequestsViewModel() : base()
        {
            RichiediRimozioneCommand = new Command<long>(RichiediRimozione);
            RemoveRichiestaCommand = new Command(OnRemoveRichiesta);
            RefreshPage = new Command(OnRefresh);

            Task.Run(() =>
            {
                Debug.WriteLine("Le mie richieste");
                Posts = new ObservableCollection<Richiesta>(DataAccess.GetRichiesteUtente());
                
            });

        }

        public void OnRefresh()
        {
            IsRefreshing = true;
            Task.Run(() =>
            {
                Posts = new ObservableCollection<Richiesta>(DataAccess.GetRichiesteUtente());
                Device.BeginInvokeOnMainThread(() => IsRefreshing = false);
            });
        }


        private void RichiediRimozione(long id_richiesta)
        {
            IdRichiesta = id_richiesta;
            ((Application.Current.MainPage as CustomNavigationPage).CurrentPage as MainPage)
                .navigator.Alert("Sicuro di voler rimuovere questa richiesta?", "conferma", RemoveRichiestaCommand, "annulla", null);

        }

        private void OnRemoveRichiesta()
        {

            Task.Run(() =>
            {
                DataAccess.DeleteRichiesta(IdRichiesta);
            });
            try
            {
                Posts.Remove(Posts.First((r) => r.id == IdRichiesta));

            }
            catch (Exception)
            {

            }
        }


    }
}
