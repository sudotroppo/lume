using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using lume.Domain;
using lume.Utility;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace lume.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {

        private bool _isReadOnly;
        public bool IsReadOnly { get => _isReadOnly; set => SetProperty(ref _isReadOnly, value); }

        private bool _isLoading;
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }

        private string _buttonText = "Modifica profilo";
        public string ButtonText { get => _buttonText; set => SetProperty(ref _buttonText, value); }


        public Command<CachedImage> SelectUserImageCommand { get; private set; }
        public Command SwitchStateCommand { get; private set; }

        private Stream SelectedImage = null;
        private string fileName = "";



        public ProfileViewModel()
        {
            SelectUserImageCommand = new Command<CachedImage>(SelectUserImage);
            SwitchStateCommand = new Command<Button>(OnStateSwitch);
            Task.Run(() => IsReadOnly = true);
        }

        public void  OnStateSwitch(Button b)
        {
            IsReadOnly = !IsReadOnly;

            if (IsReadOnly)
            {
                ButtonText = "Modifica profilo";
            }
            else
            {
                ButtonText = "Salva modifiche";

                IsLoading = true;
                Task.Run(() =>
                {
                    try
                    {
                        DataAccess.UpdateUtente(App.utente);

                        if(SelectedImage != null)
                        {
                            DataAccess.UploadUserImage(SelectedImage, fileName);
                            App.UpdateUtente();

                            Device.BeginInvokeOnMainThread(() => IsLoading = false);
                            SelectedImage = null;
                            fileName = null;
                        }

                    }catch(Exception) { }

                });

            }
            
        }


        private async void SelectUserImage(CachedImage image)
        {
            try
            {
                var file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "seleziona una immagine"
                });


                if(file != null)
                {
                    Debug.WriteLine($"fileName = {file.FileName}, fullPath = {file.FullPath}");

                    SelectedImage = await file.OpenReadAsync();
                    fileName = file.FileName;
                    image.Source = ImageSource.FromFile(file.FullPath);
                }

            }
            catch (Exception) { }

        }
    }
}
