using lume.CustomObj;
using lume.Templates;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FillRequestPage : MainPageTemplate
    {
        public FillRequestPage()
        {
            InitializeComponent();

        }

        async void Handle_Clicked(object sender, System.EventArgs e)
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


        public async void OnClickedButtonAnnulla(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();

        }

        public async void OnClickedButtonInvia(object sender, EventArgs e)
        {
            await DisplayAlert("Ottimo", "Richiesta inviata con successo!", "Ok");
            await Navigation.PopModalAsync();
            await Navigation.PushModalAsync(new TabbedHomePage());
            
        }

       

    }
}