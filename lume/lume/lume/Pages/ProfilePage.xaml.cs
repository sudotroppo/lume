
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
using lume.Utility;

namespace lume.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentTemplatedView
	{

        public ProfilePage(Navigator navigator) : base(navigator)
        {
            InitializeComponent();
            
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
                navigator.InsetPageIntoTabIndex(new NotificationsPage(navigator), 2);
                b.IsEnabled = true;
            }));

        }

        public async void OnSettingsClicked(object sender, EventArgs e)
        {
            Button b = sender as Button;

            b.IsEnabled = false;

            ProfileImage.AnchorY = 0.5;

            await Task.Run(() =>
            {
                Animation ToProfileSettings = new Animation
                {
                    {0, 1, Animations.RelativeRotation(Settings, 360, Easing.SpringOut) },
                    {0, 0.5, Animations.ScaleTo(Settings,1.4,1.4, Easing.Linear) },
                    {0.5, 1, Animations.ScaleTo(Settings, 1, 1, Easing.Linear) },
                    {0, 1, Animations.SlideOf(Settings, 35, -25,Easing.CubicInOut) },
                    {0, 1, Animations.ScaleTo(Settings, 5, 5,Easing.CubicInOut) },
                    {0, 1,Animations.FadeTo(ProfileInfoList,0,Easing.CubicInOut)},
                    {0, 1,Animations.FadeTo(ProfileImage,0,Easing.CubicInOut) },
                    {0, 1, Animations.FadeTo(FotoEdit, 0, Easing.CubicInOut) }
                };
                ToProfileSettings.Commit(this, "ToTheSettings", 1, 750, Easing.Linear, (c, v) =>
                {
                    navigator.InsetPageIntoTabIndex(new SettingsPage(navigator), 2);
                    b.IsEnabled = true;
                });
            });
        }

    }
}