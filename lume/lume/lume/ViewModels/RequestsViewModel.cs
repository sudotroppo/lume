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
    public class RequestsViewModel : BaseViewModel
    {
        public string RequestType { set; get; }


        private Richiesta _SelectedPost = new Richiesta();
        public Richiesta SelectedPost { get => _SelectedPost; set => SetProperty(ref _SelectedPost, value); }

        private bool _popped = true;
        public bool Popped { get => _popped; set => SetProperty(ref _popped, value); }

        private bool _isRichiestaPopped = false;
        public bool IsRichiestaPopped { get => _isRichiestaPopped; set => SetProperty(ref _isRichiestaPopped, value); }

        private bool _swiped = false;
        public bool Swiped { get => _swiped; set => SetProperty(ref _swiped, value); }

        private SelectionMode _selectionMode = SelectionMode.None;
        public SelectionMode SelectionMode { get => _selectionMode; set => SetProperty(ref _selectionMode, value); }

        private ObservableCollection<Richiesta> _Posts;
        public ObservableCollection<Richiesta> Posts { get => _Posts; set => SetProperty(ref _Posts, value); }

        private bool _visibile = false;
        public bool Visibile { get => _visibile; set => SetProperty(ref _visibile, value); }



        public ICommand LongPressCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand SwipeUpCommand { get; private set; }
        public ICommand SwipeDownCommand { get; private set; }
        public ICommand RichiediRimozioneCommand { get; private set; }
        public ICommand RemoveRichiestaCommand { get; private set; }



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
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Errore di connessione", "Controlla la connessione", "ok");

            }

        });


        public long IdRichiesta { private set; get; }

        public RequestsViewModel()
        {
            LongPressCommand = new Command<Richiesta>(OnLongPress);
            ClearCommand = new Command(OnClear);
            SwipeUpCommand = new Command<View>(OnSwipeUp);
            SwipeDownCommand = new Command<View>(OnSwipeDown);
            RichiediRimozioneCommand = new Command<long>(RichiediRimozione);
            RemoveRichiestaCommand = new Command(OnRemoveRichiesta);

            Task.Run(() =>
            {
                if ("partecipazioni".Equals(RequestType))
                {
                    Debug.WriteLine("Le mie partecipazioni");
                    Posts = new ObservableCollection<Richiesta>(DataAccess.GetPartecipazioniUtente());

                }
                else
                {
                    Debug.WriteLine("Le mie richieste");
                    Posts = new ObservableCollection<Richiesta>(DataAccess.GetRichiesteUtente());
                }

                Popped = false;
                Visibile = true;
            });

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

        private void RichiediRimozione(long id_richiesta)
        {
            IdRichiesta = id_richiesta;
            IsRichiestaPopped = true;

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
