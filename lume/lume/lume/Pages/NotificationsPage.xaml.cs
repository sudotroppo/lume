﻿
using lume.Templates;
using lume.Assets;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using lume.ViewModels;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationsPage : ContentTemplatedView
    {
        public NotificationsPage(Navigator navigator) : base(navigator)
        {
            InitializeComponent();
        }


        public async void OnProfileClicked (object sender, EventArgs e)
		{
            Button b = sender as Button;
            b.IsEnabled = false;

            double ProfileImageEndX = Application.Current.MainPage.Width / 2;
            double ProfileImageStartX = ProfileImage.WidthRequest / 2 + 10;

            double dx = ProfileImageEndX - ProfileImageStartX;

            Animation ToProfilepage = new Animation  // animazione di cambio pagina
            {
                {0,1, Animations.SlideOf(ToTheRight, 50, 0, Easing.CubicInOut) },
                {0,1, Animations.SlideOf(ToTheLeft, 50, 0, Easing.CubicInOut) },
                {0,1, Animations.SlideOfX(ProfileImage, dx, Easing.CubicInOut) },
                {0,1, Animations.SlideOfY(notificationsViewer, 45, Easing.CubicInOut) },
                {0,1, Animations.ScaleTo(ProfileImage, 2, 2, Easing.CubicInOut) },
                {0,1, Animations.ScaleTo(BackgroundLine, 1, 0, Easing.CubicInOut) },
                {0,1, Animations.SlideOfY(BackgroundLine, 45, Easing.CubicInOut) },
                {0,1, Animations.ScaleTo(BackgroundBoxView, 1,1, Easing.CubicInOut) },
                {0,1, Animations.FadeTo(notificationsViewer, 0, Easing.CubicInOut) },
                {0,1, Animations.SlideOf(Settings, -45,-45, Easing.CubicInOut) }
            };

            await Task.Run(() => ToProfilepage.Commit(this, "ToProfilepage", 1, 500, Easing.Linear, (c, v) =>
            {
                navigator.InsetPageIntoTabIndex(new ProfilePage(navigator), 2);

                b.IsEnabled = true;
            }));
        }

        
	}
}