using lume.CustomObj;
using lume.Domain;
using lume.Templates;
using lume.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ContentTemplatedView = lume.Templates.ContentTemplatedView;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FillRequestPage : ContentTemplatedView
    {
        public FillRequestPage(Navigator navigator) : base(navigator)
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            StepperLabel.BindingContext = this;

        }


        private async void Handle_Clicked(object sender, System.EventArgs e)
        {
            /*await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Non supportato", "Il dispositivo non supporta questa funzione", "ok");
                return;
            }
            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Full,
            };
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
            if (selectedImage == null)
            {
                await DisplayAlert("Errore", "File vuoto", "ok");
                return;
            }

            selectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());*/

            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();

            if (stream != null)
            {
                selectedImage.Source = ImageSource.FromStream(() => stream);
            }

        }

        // Funzioni relative allo stepper

        private readonly int MAX_REQUESTED_PEOPLE = 10;
        public static readonly BindableProperty RequestedPeopleValue = 
            BindableProperty.Create(nameof(RequestedPeople), typeof(int), typeof(FillRequestPage), 1);

        public int RequestedPeople
        {
            set => SetValue(RequestedPeopleValue, value);
            get => (int)GetValue(RequestedPeopleValue);
        }

        void OnStepperIncrease(object sender, EventArgs e)
        {
            if (RequestedPeople < MAX_REQUESTED_PEOPLE)
            {
                RequestedPeople++;
            }

        }

        void OnStepperDecrease(object sender, EventArgs e)
        {
            if (RequestedPeople > 1) 
            {
                RequestedPeople--;
            }
        }

        void submitRequest(object sender, EventArgs e)
        {
            PortaleLume portale = PortaleLume.getIstance();
            portale.nuovaRichiesta(
                Titolo.Text,
                Descrizione.Text,
                int.Parse(StepperLabel.Text)
                );
            OnPropertyChanged("post");
        }

    }
}