using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using lume.Domain;
using lume.Utility;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Windows.Input;

namespace lume.ViewModels
{
    public class FillRequestViewModel : BaseViewModel
    {

        private readonly int MAX_REQUESTED_PEOPLE = 10;

        private readonly ImageSource DefaultImageSource = ImageSource.FromFile("uploadImage.png");

        private bool _isLoading = false;
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }

        private Richiesta _richiesta = new Richiesta() { numeroPartecipanti = 1};
        public Richiesta Richiesta { get => _richiesta; set => SetProperty(ref _richiesta, value); }

        private KeyValuePair<string, Stream> _imageStream = new KeyValuePair<string, Stream>();

        private ImageSource _immagine = ImageSource.FromFile("uploadImage.png");
        public ImageSource Immagine { get => _immagine; set => SetProperty(ref _immagine, value); }

        private Image _selectedImage = new Image();
        public Image SelectedImage { get => _selectedImage; set => SetProperty(ref _selectedImage, value); }


        private int _numeroPartecipanti;
        public int NumeroPartecipanti
        {
            get => _numeroPartecipanti;
            set => SetProperty(ref _numeroPartecipanti, value, "NumeroPartecipanti", () => _richiesta.numeroPartecipanti = value);
        }


        public ICommand SendRequestCommand { get; private set; }
        public ICommand IncreaseCommand { get; private set; }
        public ICommand DecreaseCommand { get; private set; }
        public ICommand AddImageFromCameraCommand { get; private set; }
        public ICommand AddImageFromGalleryCommand { get; private set; }
        public ICommand RemoveImageCommand { get; private set; }
        public ICommand SelectImageCommand { get; private set; }

        public FillRequestViewModel()
        {
            SendRequestCommand = new Command(OnSendRequest);
            IncreaseCommand = new Command(OnIncreas);
            DecreaseCommand = new Command(OnDecrease);
            AddImageFromCameraCommand = new Command(AddImageFromCamera);
            AddImageFromGalleryCommand = new Command(AddImageFromGallery);
            RemoveImageCommand = new Command(RemoveImage);
            SelectImageCommand = new Command<Image>(OnSelectedImage);
            NumeroPartecipanti = 1;
        }

        private void OnSelectedImage(Image obj)
        {
            if (obj == null || DefaultImageSource.Equals(obj.Source) )
            {
                return;
            }

            if (obj.Equals(SelectedImage))
            {
                SelectedImage.ScaleTo(1, 200, Easing.CubicInOut);
                SelectedImage = null;
            }
            else if(SelectedImage != null)
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

            if(Richiesta.descrizione == null || Richiesta.titolo == null
                || Richiesta.titolo.Trim().Equals("") || Richiesta.titolo.Trim().Equals(""))
            {
                navigator.Alert("Devi riempire i campi prima di inviare una richiesta di aiuto", "", "ok");
                return;
            }

            IsLoading = true;

            Task.Run(async () =>
            {
                long id_richiesta = DataAccess.NewRichiesta(Richiesta);

                Richiesta = new Richiesta { numeroPartecipanti = 1 };
                NumeroPartecipanti = 1;

                if (!DefaultImageSource.Equals(Immagine))
                {
                    await Task.Run(() =>
                    {
                        DataAccess.UploadRequestImage(_imageStream.Value, _imageStream.Key, id_richiesta);
                    });

                    Immagine = DefaultImageSource;

                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
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

                Debug.WriteLine("Apro la fotocamera");
                if (MediaPicker.IsCaptureSupported)
                {
                    var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = "Scatta una foto"
                    });

                    if (result != null)
                    {
                        Stream stream = await result.OpenReadAsync();

                        string fileName = $"{DateTime.Now.GetHashCode()}.jpg";
                        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        Debug.WriteLine($"fileName = {fileName}, filePath = {filePath}");

                        Debug.WriteLine("Aggiungo l'immagine");
                        FileUtility.SaveStreamAsFile(filePath, stream, fileName);

                        SetImage(filePath + "/" + fileName);
                        _imageStream = new KeyValuePair<string, Stream>(fileName, stream);

                    }
                }

            }
            catch (Exception) { }
        }


        public async void AddImageFromGallery()
        {
            if(Immagine != null)
            {
                try
                {
                    Debug.WriteLine("Apro la galleria");
                    var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                    {
                        Title = "Scatta una foto"
                    });

                    if(result != null)
                    {
                        string fileFullPath = result.FullPath;

                        Debug.WriteLine("aggiungo l'immagine");
                        SetImage(fileFullPath);
                        _imageStream = new KeyValuePair<string, Stream>(result.FileName, await result.OpenReadAsync());

                    }

                }
                catch (Exception) { }

            }

        }


        private void SetImage(string fileName)
        {
            Immagine = ImageSource.FromFile(fileName);

        }

        public void RemoveImage()
        {
            if(SelectedImage != null && Immagine != null)
            {
                Debug.WriteLine("rimuovo l'immagine");
                Immagine = null;
                SelectedImage.ScaleTo(1, 200, Easing.CubicInOut);
                SelectedImage = null;
                Immagine = DefaultImageSource;

            }
        }

    }
}
