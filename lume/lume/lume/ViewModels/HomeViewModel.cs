using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using lume.CustomObj;
using lume.Domain;
using lume.Utility;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class HomeViewModel : BaseViewModel
    {
        private bool _IsRefreshing;
        public bool IsRefreshing { get => _IsRefreshing; set => SetProperty(ref _IsRefreshing, value); }

        private Richiesta _SelectedPost = new Richiesta();
        public Richiesta SelectedPost { get => _SelectedPost; set => SetProperty(ref _SelectedPost, value); }

        private bool _popped = true;
        public bool Popped { get => _popped; set => SetProperty(ref _popped, value); }

        private bool _swiped = false;
        public bool Swiped { get => _swiped; set => SetProperty(ref _swiped, value); }

        private SelectionMode _selectionMode = SelectionMode.None;
        public SelectionMode SelectionMode { get => _selectionMode; set => SetProperty(ref _selectionMode, value); }

        private ObservableCollection<Richiesta> _Posts;
        public ObservableCollection<Richiesta> Posts { get => _Posts; set => SetProperty(ref _Posts, value); }

        private bool _visibile = false;
        public bool Visibile { get => _visibile; set => SetProperty(ref _visibile, value); }



        public Command<Richiesta> LongPressCommand { get; private set; }

        //public Command<Richiesta> PressedCommand { get; private set; }

        public Command ClearCommand { get; private set; }

        public Command<View> SwipeUpCommand { get; private set; }

        public Command<View> SwipeDownCommand { get; private set; }



        public ICommand SendPartecipation => new Command<long>(async (id) =>
        {
            bool check = true;

            try
            {

                await Task.Run(() =>
                {
                    check = DataAccess.PartecipaAProposta(id);
                });

                if (check)
                {
                    await App.Current.MainPage.DisplayAlert("Congratulazioni", "hai partecipato con successo alla richiesta di aiuto", "continua");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Ops..", "controlla di non aver già partecipato o che la richiesta non sia al completo", "ok");
                }
            }
            catch(Exception)
            {
                await App.Current.MainPage.DisplayAlert("Errore di connessione", "Controlla la connessione", "ok");

            }

        });

        public ICommand RefreshPage => new Command(async () =>
        {
            Debug.WriteLine($"{IsRefreshing}");

            await Task.Run(() => Posts = new ObservableCollection<Richiesta>(DataAccess.GetAllRichieste()));

            IsRefreshing = false;
            Debug.WriteLine($"{IsRefreshing}");
        });



        public HomeViewModel()
        {
            LongPressCommand = new Command<Richiesta>(OnLongPress);
            ClearCommand = new Command(OnClear);
            SwipeUpCommand = new Command<View>(OnSwipeUp);
            SwipeDownCommand = new Command<View>(OnSwipeDown);

            Task.Run(() =>
            {
                Posts = new ObservableCollection<Richiesta>(DataAccess.GetAllRichieste());

                Popped = false;
                Visibile = true;
            });

        }

        private void OnSwipeUp(View obj)
        {
            if(Popped && !Swiped)
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
