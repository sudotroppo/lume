using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using lume.Domain;
using lume.Utility;
using Xamarin.Forms;

namespace lume.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private bool _IsRefreshing;

        public bool IsRefreshing
        {
            get { return _IsRefreshing; }

            set
            {
                _IsRefreshing = value;
                OnPropertyChanged();
            }
        }

        private Richiesta _SelectedPost = new Richiesta();

        public Richiesta SelectedPost
        {
            get { return _SelectedPost; }

            set
            {
                _SelectedPost = value;
                OnPropertyChanged();
            }
        }

        private SelectionMode _selectionMode = SelectionMode.None;

        public SelectionMode SelectionMode { get => _selectionMode; set => SetProperty(ref _selectionMode, value); }

        private static readonly double hiddenPosition = Application.Current.MainPage.Height;

        private double _posizione = hiddenPosition;

        public double Posizione { get => _posizione; set => SetProperty(ref _posizione, value); }


        public Command ShareCommand { get; set; }

        public Command<Richiesta> LongPressCommand { get; private set; }

        public Command ClearCommand { get; private set; }

        public Command<Richiesta> PressedCommand { get; private set; }

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
                    OnPropertyChanged();
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

        private ObservableCollection<Richiesta> _Posts;

        public ObservableCollection<Richiesta> Posts
        {
            get { return _Posts; }

            set
            {
                _Posts = value;
                OnPropertyChanged();
            }
        }


        public HomeViewModel()
        {
            LongPressCommand = new Command<Richiesta>(OnLongPress);
            ClearCommand = new Command(OnClear);

            Task.Run(() =>
            {
                Posts = new ObservableCollection<Richiesta> (DataAccess.GetAllRichieste());
            });
        }

        private void OnLongPress(Richiesta obj)
        {
            Debug.WriteLine("LongPressed");

            if (_selectionMode == SelectionMode.None)
            {
                SelectionMode = SelectionMode.Single;
                SelectedPost = obj;
            }
        }

        private void OnClear()
        {
            SelectionMode = SelectionMode.None;
        }

        public static Animation SlideOfY(double posizione, double dy, Easing easing)
        {
            return new Animation(c =>
            { 
                posizione += c * dy;
            },
            0, 1, easing ?? Easing.Linear);
        }
    }
}
