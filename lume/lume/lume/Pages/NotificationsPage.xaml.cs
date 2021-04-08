
using lume.Templates;
using lume.Assets;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationsPage : MainPageTemplate
    {
        public NotificationsPage()
        {
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BackgroundLine.ScaleX = 0;
            
            Task.Run(() => AnimationFactory.ScaleTo(BackgroundLine, 1, 1, Easing.CubicInOut).Commit(this, "OnAppearing", 1, 500, Easing.Linear));
        }

        public async void OnProfileClicked (object sender, EventArgs e)
		{
            Button b = (sender as Button);
            b.IsEnabled = false;
            Navigation.InsertPageBefore(new ProfilePage(), this);
            double ProfileImageEndX = (Application.Current.MainPage.Width / 2);
            double ProfileImageStartX = ProfileImage.WidthRequest/2 - 10;
            double dx = ProfileImageEndX - ProfileImageStartX;
            Animation ToProfilepage = new Animation  // animazione di cambio pagina
            {
                {0,1, AnimationFactory.SlideOf(ToTheRight, 50, 0, Easing.CubicInOut) },
                {0,1, AnimationFactory.SlideOf(ToTheLeft, 50, 0, Easing.CubicInOut) },
                {0,1, AnimationFactory.SlideOfX(ProfileImage, dx, Easing.CubicInOut) },
                {0,1, AnimationFactory.ScaleTo(ProfileImage, 2, 2, Easing.CubicInOut) },
                {0,1, AnimationFactory.ScaleTo(BackgroundLine, 1, 0, Easing.CubicInOut) },
                {0,1, AnimationFactory.ScaleTo(BackgroundBoxView, 1,1, Easing.CubicInOut) }
            };

            await Task.Run(() => ToProfilepage.Commit(this, "Prova", 1, 500, Easing.Linear, async (c, v) =>
             {
                 await Navigation.PopAsync(false);
                 b.IsEnabled = true;
             }));
        }

        
	}
}