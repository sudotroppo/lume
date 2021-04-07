
using lume.Assets;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationsPage : ContentPage
    {
        private static Animation ToProfilepage;

        public NotificationsPage()
        {
            InitializeComponent();
            double dx = (Application.Current.MainPage.Width / 2) - (ProfileImage.WidthRequest/2 - 10); // 10 = margine sinistro della pagina
            ToProfilepage = new Animation  // animazione di cambio pagina
            {
                {0,0.5, AnimationFactory.SlideOf(ToTheRight, 50, 0, Easing.SpringOut) },
                {0,1, AnimationFactory.SlideOf(ToTheLeft, 50, 0, Easing.SpringOut) },
                {0,1, AnimationFactory.SlideOfX(ProfileImage, dx, Easing.CubicInOut) },
                {0,1, AnimationFactory.ScaleTo(ProfileImage, 2, 2, Easing.CubicInOut) }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BackgroundLine.ScaleX = 0;
            Task.Run(() => AnimationFactory.ScaleTo(BackgroundLine, 1, 1, Easing.SpringOut).Commit(this, "OnAppearing", 1, 500, Easing.Linear));
        }

        public async void OnProfileClicked (object sender, EventArgs e)
		{
            Button b = (sender as Button);
            b.IsEnabled = false;
            Navigation.InsertPageBefore(new ProfilePage(), this);

            await Task.Run(() => ToProfilepage.Commit(this, "Prova", 1, 500, Easing.SinIn,async (c, v) =>
            {
                await Navigation.PopAsync();
                b.IsEnabled = true;
            }));
        }

        
	}
}