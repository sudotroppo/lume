using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class FillRequestViewModel : BaseViewModel
    {

        private readonly int MAX_REQUESTED_PEOPLE = 10;

        private Richiesta _richiesta = new Richiesta();
        public Richiesta Richiesta { get => _richiesta; set => SetProperty(ref _richiesta, value); }

        private List<Stream> _imagesStream = new List<Stream>();
        public List<Stream> ImagesStream { get => _imagesStream; set => SetProperty(ref _imagesStream, value); }

        private ObservableCollection<ImageSource> _images = new ObservableCollection<ImageSource>();
        public ObservableCollection<ImageSource> Images { get => _images; set => SetProperty(ref _images, value); }

        private CachedImage _selectedImage = new CachedImage();
        public CachedImage SelectedImage { get => _selectedImage; set => SetProperty(ref _selectedImage, value); }

        private int _numeroPartecipanti = 1;
        public int NumeroPartecipanti
        {
            get => _numeroPartecipanti;
            set => SetProperty(ref _numeroPartecipanti, value, "NumeroPartecipanti", () => _richiesta.numeroPartecipanti = value);
        }

        public Command SendRequestCommand { get; private set; }
        public Command IncreaseCommand { get; private set; }
        public Command DecreaseCommand { get; private set; }
        public Command AddImageFromCameraCommand { get; private set; }
        public Command AddImageFromGalleryCommand { get; private set; }
        public Command RemoveImageCommand { get; private set; }
        public Command<CachedImage> SelectImageCommand { get; private set; }

        public FillRequestViewModel()
        {
            SendRequestCommand = new Command(OnSendRequest);
            IncreaseCommand = new Command(OnIncreas);
            DecreaseCommand = new Command(OnDecrease);
            AddImageFromCameraCommand = new Command(AddImageFromCamera);
            AddImageFromGalleryCommand = new Command(AddImageFromGallery);
            RemoveImageCommand = new Command(RemoveImage);
            SelectImageCommand = new Command<CachedImage>(OnSelectedImage);

            SelectedImage = null;
        }

        private void OnSelectedImage(CachedImage obj)
        {
            if (obj.Equals(SelectedImage))
            {
                SelectedImage.ScaleTo(1, 200, Easing.CubicInOut);
                SelectedImage = null;
            }
            else if(obj != null)
            {
                SelectedImage.ScaleTo(1, 200, Easing.CubicInOut);
                SelectedImage = obj;
                Task.Run(async () =>
                {
                    await Task.Delay(200);
                    await obj.ScaleTo(.8, 200, Easing.CubicInOut);
                });
            }
            else
            {
                SelectedImage = obj;
                obj.ScaleTo(.8, 200, Easing.CubicInOut);
            }
        }

        public void OnSendRequest()
        {
            Task.Run(() =>
            {
                DataAccess.NewRichiesta(Richiesta);
                Richiesta = new Richiesta { numeroPartecipanti = 1 };
                NumeroPartecipanti = 1;
            });
        }

        public void OnIncreas()
        {
            if (NumeroPartecipanti < MAX_REQUESTED_PEOPLE)
            {
                Debug.WriteLine("aumento!");
                NumeroPartecipanti++;
            }
        }

        public void OnDecrease()
        {
            if (NumeroPartecipanti > 1)
            {
                Debug.WriteLine("decremento!");
                NumeroPartecipanti--;
            }
        }

        public async void AddImageFromCamera()
        {
            try
            {
                if (MediaPicker.IsCaptureSupported)
                {
                    var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = "Scatta una foto"
                    });

                    if(result != null)
                    {
                        Stream stream = await result.OpenReadAsync();

                        string fileName = $"{DateTime.Now.GetHashCode()}.jpg";
                        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        Debug.WriteLine($"fileName = {fileName}, filePath = {filePath}");

                        FileUtility.SaveStreamAsFile(filePath, stream, fileName);
                        Images.Add(ImageSource.FromFile(filePath + "/" + fileName));

                    }
                }

            }
            catch (Exception) { }
        }

        public async void AddImageFromGallery()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Scatta una foto"
                });

                if(result != null)
                {
                    string fileFullPath = result.FullPath;

                    Images.Add(ImageSource.FromFile(fileFullPath));

                }

            }
            catch (Exception) { }
        }

        public void RemoveImage()
        {
            if(SelectedImage != null)
            {
                Images.Remove(SelectedImage.Source);
                SelectedImage = null;

            }
        }

    }
}
