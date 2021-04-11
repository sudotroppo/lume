
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using lume;
using lume.Templates;
using System.Collections.Generic;
using System;
using lume.Assets;
using System.Threading.Tasks;

namespace lume.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{

        private bool EditMode = false;
        private readonly IList<View> InfoList;

        public ProfilePage()
        {
            InitializeComponent();
            InfoList = InfoStack.Children;

        }

        private void SwitchButtonState(string status, Button button)
        {

            var verdeLume = (Color)Application.Current.Resources["VerdeLume"];
            var nero = (Color)Application.Current.Resources["Nero"];
            var grigioChiaro = (Color)Application.Current.Resources["GrigioLume"];

            // Quando l'utente si trova in stato di modifica
            if (status == "edit")
            {
                button.Text = "Fatto";
                button.TextColor = grigioChiaro;
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

            EditMode = !EditMode;

            if (EditMode)
                SwitchButtonState("edit", button);
            else
                SwitchButtonState("default", button);

            foreach (var child in InfoList)
            {
                if (child is InfoView info)
                {
                    info.IsEditable = EditMode;
                }
            }


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(() =>
            {
                AnimationFactory.CascadeToTheChildren(ProfileInfoList, (v) =>
                {
                    v.ScaleX = 0;
                    v.ScaleY = 0;
                    return AnimationFactory.ScaleTo(v, 1, 1, Easing.CubicInOut);
                }).Commit(this, "OnAppearing", 1, 1400);
            });

        }

        public async void OnBackButtonCliked(object sender, EventArgs e)
        {
            Button b = (sender as Button);
            b.IsEnabled = false;
            Navigation.InsertPageBefore(new NotificationsPage(), this);
            double ProfileImageStartX = (Application.Current.MainPage.Width / 2);
            double ProfileImageEndX = 10 + (ProfileImage.WidthRequest / 4f);
            double dx = ProfileImageEndX - ProfileImageStartX;

            Animation ToNotificationPage = new Animation  // animazione di cambio pagina
            {
                {0, 1, AnimationFactory.SlideOfX(ProfileImage, dx, Easing.CubicInOut) },
                {0, 1, AnimationFactory.ScaleTo(ProfileImage, 0.5, 0.5,Easing.CubicInOut) },
                {0, 1, AnimationFactory.SlideOfX(ToTheRight, -50, Easing.CubicInOut) },
                {0, 1, AnimationFactory.SlideOfX(ToTheLeft, -50, Easing.CubicInOut) },
                {0, 1, AnimationFactory.ScaleTo(BackgroundBoxView, 1,0,Easing.CubicInOut) }
            };

            await Task.Run(() => ToNotificationPage.Commit(this, "Prova", 1, 500, Easing.Linear, async (c, v) =>
            {
                await Navigation.PopAsync(false);
                b.IsEnabled = true;
            }));

        }

        public async void OnSettingsClicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.IsEnabled = false;
            Navigation.InsertPageBefore(new SettingsPage(), this);
            await Task.Run(() =>
            {
                Animation ToProfileSettings = new Animation
                {
                    {0,1, AnimationFactory.RelativeRotation(b, 360, Easing.SpringOut) },
                    {0,0.5, AnimationFactory.ScaleTo(b,1.4,1.4, Easing.Linear) },
                    {0.5,1, AnimationFactory.ScaleTo(b, 1, 1, Easing.Linear) }
                };
                ToProfileSettings.Commit(this, "ToTheSettings", 1, 750, Easing.Linear, (c, v) =>
                {
                    Navigation.PopAsync(false);
                    b.IsEnabled = true;
                });
            });
        }

    }
}