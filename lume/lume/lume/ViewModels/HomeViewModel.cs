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
        private bool _IsRefreshing;
        public bool IsRefreshing { get => _IsRefreshing; set => SetProperty(ref _IsRefreshing, value); }


        public ICommand SendPartecipation { set; get; }


        public ICommand RefreshPage => new Command(async () =>
        {
            Debug.WriteLine($"{IsRefreshing}");

            await Task.Run(() => Posts = new ObservableCollection<Richiesta>(DataAccess.GetAllRichieste()));

            IsRefreshing = false;
            Debug.WriteLine($"{IsRefreshing}");
        });



        public HomeViewModel()
        {
            SendPartecipation = new Command<long>(OnSendRequest);
            _ = Task.Run(() =>
              {
                  Posts = new ObservableCollection<Richiesta>(DataAccess.GetAllRichieste());

              });

        }

        public async void OnSendRequest(long id)
        {
            bool check = true;
            var mainPage = (Application.Current.MainPage as CustomNavigationPage).CurrentPage as MainPage;

            try
            {

                await Task.Run(() =>
                {
                    check = DataAccess.PartecipaAProposta(id);
                });


                if (check)
                {
                    Posts.First((r) => r.id.Equals(id)).addCandidato(App.utente);
                    mainPage.navigator.Alert("Hai partecipato con successo alla richiesta di aiuto", "", "ok");

                }
                else
                {
                    mainPage.navigator.Alert("Ops.. controlla di non aver già partecipato o che la richiesta non sia al completo", "", "ok");
                }
            }
            catch(Exception e)
            {
                mainPage.navigator.Alert("Errore di connessione, controlla la connessione", "", "ok");
                Debug.WriteLine($"{e.Message}");

            }

        }
    }
}
