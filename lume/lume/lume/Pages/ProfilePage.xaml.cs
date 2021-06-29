
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using lume;
using lume.Templates;
using System.Collections.Generic;
using System;
using lume.Assets;
using System.Threading.Tasks;
using lume.ViewModels;
using System.Windows.Input;

namespace lume.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentTemplatedView
	{

        private bool ReadOnly = true;
        private readonly IList<View> InfoList;

        public ICommand ChangeFoto => new Command(async () => await DisplayAlert("Da implementare!", "si dovrebbe aprire la galleria", "Ok"));


        public ProfilePage(Navigator navigator) : base(navigator)
        {
            InitializeComponent();
            BindingContext = this;
            InfoList = InfoStack.Children;

        }

        private void SwitchButtonState(string status, Button button)
        {

            var verdeLume = (Color)Application.Current.Resources["VerdeLume"];
            var nero = (Color)Application.Current.Resources["Nero"];
            var grigioChiaro = (Color)Application.Current.Resources["GrigioLume"];
            var bianco = (Color)Application.Current.Resources["BiancoLume"];

            // Quando l'utente si trova in stato di modifica
            if (status == "edit")
            {
                button.Text = "Fatto";
                button.TextColor = bianco;
                button.BackgroundColor = verdeLume;
            }

            // Stato di default
            else
            {
                button.Text = "Modifica profilo";
                button.TextColor = nero;
                button.BackgroundColor = grigioChiaro;
            }

        }

        public void OnModifyProfileClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            ReadOnly = !ReadOnly;

            if (!ReadOnly)
            {
                ToTheLeft.IsEnabled = false;
                Settings.IsEnabled = false;
                SwitchButtonState("edit", button);

            }
            else
            {
                ToTheLeft.IsEnabled = true;
                Settings.IsEnabled = true;
                SwitchButtonState("default", button);
            }

            foreach (var child in InfoList)
            {
                if (child is InfoView info)
                {
                    info.IsReadOnly = ReadOnly;
                }
            }
        }

        public async void OnBackButtonCliked(object sender, EventArgs e)
        {
            Button b = (sender as Button);
            b.TextColor = b.TextColor;
            b.IsEnabled = false;

            double ProfileImageStartX = (Application.Current.MainPage.Width / 2);
            double ProfileImageEndX = 10 + (ProfileImage.WidthRequest / 4f);
            double dx = ProfileImageEndX - ProfileImageStartX;

            Animation ToNotificationPage = new Animation  // animazione di cambio pagina
            {
                {0, 1, Animations.SlideOfX(ProfileImage, dx, Easing.CubicInOut) },
                {0, 1, Animations.ScaleTo(ProfileImage, 0.5, 0.5,Easing.CubicInOut) },
                {0, 1, Animations.SlideOfX(ToTheRight, -50, Easing.CubicInOut) },
                {0, 1, Animations.SlideOfX(ToTheLeft, -50, Easing.CubicInOut) },
                {0, 1, Animations.ScaleTo(BackgroundBoxView, 1,0,Easing.CubicInOut) },
                {0, 1, Animations.FadeTo(FotoEdit, 0, Easing.CubicInOut) },
                {0, 1, Animations.FadeTo(UpdateButton, 0, Easing.CubicInOut) },
                {0, 1, Animations.FadeTo(InfoStack, 0, Easing.CubicInOut) },
                {0,1, Animations.SlideOf(Settings, 45, 45, Easing.CubicInOut) }
            };

            await Task.Run(() => ToNotificationPage.Commit(this, "Prova", 1, 500, Easing.Linear, (c, v) =>
            {
                navigator.PushAsync(new NotificationsPage(navigator));
                b.IsEnabled = true;
            }));

        }

        public async void OnSettingsClicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.IsEnabled = false;

            await Task.Run(() =>
            {
                Animation ToProfileSettings = new Animation
                {
                    {0,1, Animations.RelativeRotation(b, 360, Easing.SpringOut) },
                    {0,0.5, Animations.ScaleTo(b,1.4,1.4, Easing.Linear) },
                    {0.5,1, Animations.ScaleTo(b, 1, 1, Easing.Linear) }
                };
                ToProfileSettings.Commit(this, "ToTheSettings", 1, 750, Easing.Linear, (c, v) =>
                {
                    navigator.PushAsync(new SettingsPage(navigator));
                    b.IsEnabled = true;
                });
            });
        }

    }
}