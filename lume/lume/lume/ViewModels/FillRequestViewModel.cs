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

namespace lume.ViewModels
{
    public class FillRequestViewModel : BaseViewModel
    {

        private readonly int MAX_REQUESTED_PEOPLE = 10;
        private readonly int MAX_IMAGES = 10;

        private readonly ImageSource DefaultImageSource = ImageSource.FromFile("uploadImage.png");

        private bool _isLoading = false;
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }

        private Richiesta _richiesta = new Richiesta() { numeroPartecipanti = 1};
        public Richiesta Richiesta { get => _richiesta; set => SetProperty(ref _richiesta, value); }

        private Dictionary<string, Stream> _imagesStream = new Dictionary<string, Stream>();

        private ObservableRangeCollection<ImageSource> _images;
        public ObservableRangeCollection<ImageSource> Images { get => _images; set => SetProperty(ref _images, value); }

        private Image _selectedImage = null;
        public Image SelectedImage { get => _selectedImage; set => SetProperty(ref _selectedImage, value); }


        private int _numeroPartecipanti;
        public int NumeroPartecipanti
        {
            get => _numeroPartecipanti;
            set => SetProperty(ref _numeroPartecipanti, value, "NumeroPartecipanti", () => _richiesta.numeroPartecipanti = value);
        }

        private int ImagesCount = 0;

        public Command SendRequestCommand { get; private set; }
        public Command IncreaseCommand { get; private set; }
        public Command DecreaseCommand { get; private set; }
        public Command AddImageFromCameraCommand { get; private set; }
        public Command AddImageFromGalleryCommand { get; private set; }
        public Command RemoveImageCommand { get; private set; }
        public Command<Image> SelectImageCommand { get; private set; }

        public FillRequestViewModel()
        {
            Images = new ObservableRangeCollection<ImageSource>();
            Images.Add(DefaultImageSource);
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
            if (obj.Source.Equals(DefaultImageSource))
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
            IsLoading = true;
            Task.Run(async () =>
            {
                long id_richiesta = DataAccess.NewRichiesta(Richiesta);
                Richiesta = new Richiesta { numeroPartecipanti = 1 };
                NumeroPartecipanti = 1;
                List<Task> tasks = new List<Task>(Images.Count);
                int i = 0;
                foreach (KeyValuePair<string, Stream> keyValuePair in _imagesStream)
                {
                    
                    var t = Task.Run(() =>
                    {
                        Debug.Write($"i = {i}");
                        DataAccess.UploadRequestImage(keyValuePair.Value, keyValuePair.Key, id_richiesta);
                    });

                    tasks.Add(t);
                    i++;
                }

                Task.WaitAll(tasks.ToArray());

                Images.Clear();
                Images.Add(DefaultImageSource);

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
            if (ImagesCount <= MAX_IMAGES)
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

                            AddImage(filePath + "/" + fileName);
                            _imagesStream.Add(fileName, stream);

                        }
                    }

                }
                catch (Exception) { }
            }
        }


        public async void AddImageFromGallery()
        {
            if(Images.Count <= MAX_IMAGES)
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
                        AddImage(fileFullPath);
                        _imagesStream.Add(result.FileName, await result.OpenReadAsync());

                    }

                }
                catch (Exception) { }

            }

        }


        private void AddImage(string fileName)
        {

            if (Images.Contains(DefaultImageSource))
            {
                Images.Remove(DefaultImageSource);
            }

            Images.Add(ImageSource.FromFile(fileName));

        }

        public void RemoveImage()
        {
            if(SelectedImage != null && Images.Count > 0)
            {
                Debug.WriteLine("rimuovo l'immagine");
                Images.Remove(SelectedImage.Source);
                SelectedImage.ScaleTo(1, 200, Easing.CubicInOut);
                SelectedImage = null;

                if(Images.Count == 0)
                {
                    Images.Add(DefaultImageSource);
                }

            }
        }

    }
}
