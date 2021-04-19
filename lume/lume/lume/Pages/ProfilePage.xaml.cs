
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using lume;
using lume.Templates;
using System.Collections.Generic;
using System;
using lume.Assets;
using System.Threading.Tasks;
using lume.ViewModels;

namespace lume.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentTemplatedView
	{

        private bool EditMode = false;
        private readonly IList<View> InfoList;

        public ProfilePage(MainPageTemplate Control) : base(Control)
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
                {0,1, Animations.SlideOf(Settings, 45, 45, Easing.CubicInOut) }
            };

            await Task.Run(() => ToNotificationPage.Commit(this, "Prova", 1, 500, Easing.Linear, async (c, v) =>
            {
                Control.SetValue(MainPageTemplate.TemplateContentProperty, new NotificationsPage(Control).Content);
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
                    Control.SetValue(MainPageTemplate.TemplateContentProperty, new SettingsPage(Control).Content);
                    b.IsEnabled = true;
                });
            });
        }

    }
}