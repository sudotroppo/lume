using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using lume.CustomObj;
using lume.Domain;
using lume.Pages;
using lume.Utility;
using Xamarin.Forms;

namespace lume.ViewModels
{
    public class RichiestaPopUpViewModel : BaseViewModel
    {


        private Richiesta _SelectedPost = new Richiesta() { creatore = new Utente() };
        public Richiesta SelectedPost { get => _SelectedPost; set => SetProperty(ref _SelectedPost, value); }

        private bool _IsRefreshing = false;
        public bool IsRefreshing { get => _IsRefreshing; set => SetProperty(ref _IsRefreshing, value); }

        private bool _popped = false;
        public bool Popped { get => _popped; set => SetProperty(ref _popped, value); }

        private bool _swiped = false;
        public bool Swiped { get => _swiped; set => SetProperty(ref _swiped, value); }

        private SelectionMode _selectionMode = SelectionMode.None;
        public SelectionMode SelectionMode { get => _selectionMode; set => SetProperty(ref _selectionMode, value); }

        private ObservableCollection<Richiesta> _Posts = new ObservableCollection<Richiesta>();
        public ObservableCollection<Richiesta> Posts { get => _Posts; set => SetProperty(ref _Posts, value); }

        public ICommand SendPartecipation { set; get; }



        public ICommand LongPressCommand { get; private set; }
        //public ICommand<Richiesta> PressedCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand SwipeUpCommand { get; private set; }
        public ICommand SwipeDownCommand { get; private set; }

        public RichiestaPopUpViewModel()
        {
            LongPressCommand = new Command<Richiesta>(OnLongPress);
            ClearCommand = new Command(OnClear);
            SwipeUpCommand = new Command<View>(OnSwipeUp);
            SwipeDownCommand = new Command<View>(OnSwipeDown);
            SendPartecipation = new Command<long>(OnSendRequest);
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
                    Richiesta richiesta = Posts.First((r) => r.id.Equals(id));
                    richiesta.addCandidato(App.utente);
                    mainPage.navigator.Alert("Hai partecipato con successo alla richiesta di aiuto", "", "ok");

                }
                else
                {

                    Richiesta richiesta = Posts.First((r) => r.id.Equals(id));
                    richiesta.removeCandidato(App.utente);

                    mainPage.navigator.Alert("Ti sei ritirato dalla richiesta di aiuto", "", "ok");
                }
            }
            catch (Exception e)
            {
                mainPage.navigator.Alert("Errore di connessione, controlla la connessione", "", "ok");
                Debug.WriteLine($"{e.Message}");

            }

        }

        private void OnSwipeUp(View obj)
        {
            if (Popped && !Swiped)
            {
                obj.TranslateTo(0, 0, 300, Easing.CubicInOut);
                Swiped = true;
            }

        }

        private void OnSwipeDown(View obj)
        {
            if (Popped && Swiped)
            {
                obj.TranslateTo(0, (0.35) * obj.Height, 300, Easing.CubicInOut);
                Swiped = false;
            }
            else if (Popped && !Swiped)
            {
                Popped = false;
            }

        }

        private void OnLongPress(Richiesta obj)
        {
            Debug.WriteLine("LongPressed");
            Debug.WriteLine($"id = {obj.id}");

            if (obj.Equals(SelectedPost) && Popped) { return; }

            if (!Popped)
            {

                SelectedPost = obj;
                Popped = true;

            }
            else
            {
                Popped = false;

                Task.Run(async () =>
                {
                    await Task.Delay(500);
                    SelectedPost = obj;
                    Popped = true;
                });
            }
        }

        private void OnClear()
        {
            if (Popped)
            {
                Debug.WriteLine("Clear");
                Popped = false;

            }
        }

    }
}
