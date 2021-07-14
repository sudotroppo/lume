using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using lume.Domain;
using Xamarin.Forms;

namespace lume.ViewModels
{
    public class RichiestaPopUpViewModel : BaseViewModel
    {


        private Richiesta _SelectedPost = new Richiesta();
        public Richiesta SelectedPost { get => _SelectedPost; set => SetProperty(ref _SelectedPost, value); }

        private bool _popped = false;
        public bool Popped { get => _popped; set => SetProperty(ref _popped, value); }

        private bool _swiped = false;
        public bool Swiped { get => _swiped; set => SetProperty(ref _swiped, value); }

        private SelectionMode _selectionMode = SelectionMode.None;
        public SelectionMode SelectionMode { get => _selectionMode; set => SetProperty(ref _selectionMode, value); }

        private ObservableCollection<Richiesta> _Posts;
        public ObservableCollection<Richiesta> Posts { get => _Posts; set => SetProperty(ref _Posts, value); }


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
