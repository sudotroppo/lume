using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using lume.Utility;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace lume.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {


        public Command<CachedImage> SelectUserImageCommand { get; set; }

        public ProfileViewModel()
        {
            SelectUserImageCommand = new Command<CachedImage>(SelectUserImage);
        }

        private async void SelectUserImage(CachedImage image)
        {

            var file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "seleziona una immagine"
            });


            if(file != null)
            {
                Debug.WriteLine($"fileName = {file.FileName}, fullPath = {file.FullPath}");

                Stream stream = await file.OpenReadAsync();


                await Task.Run(() =>
                {
                    DataAccess.UploadUserImage(stream, file.FileName);

                    _ = App.UpdateUtente();
                    OnPropertyChanged();
                });

            }
        }
    }
}
