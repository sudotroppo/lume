
using lume.Templates;
using lume.Assets;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationsPage : ContentTemplatedView
    {
        public NotificationsPage(MainPageTemplate Control) : base(Control)
        {
            InitializeComponent();
            
        }


        public async void OnProfileClicked (object sender, EventArgs e)
		{
            Button b = (sender as Button);
            b.IsEnabled = false;
            double ProfileImageEndX = (Application.Current.MainPage.Width / 2);
            double ProfileImageStartX = ProfileImage.WidthRequest/2 - 10;
            double dx = ProfileImageEndX - ProfileImageStartX;
            Animation ToProfilepage = new Animation  // animazione di cambio pagina
            {
                {0,1, Animations.SlideOf(ToTheRight, 50, 0, Easing.CubicInOut) },
                {0,1, Animations.SlideOf(ToTheLeft, 50, 0, Easing.CubicInOut) },
                {0,1, Animations.SlideOfX(ProfileImage, dx, Easing.CubicInOut) },
                {0,1, Animations.ScaleTo(ProfileImage, 2, 2, Easing.CubicInOut) },
                {0,1, Animations.ScaleTo(BackgroundLine, 1, 0, Easing.CubicInOut) },
                {0,1, Animations.ScaleTo(BackgroundBoxView, 1,1, Easing.CubicInOut) }
            };

            await Task.Run(() => ToProfilepage.Commit(this, "Prova", 1, 500, Easing.Linear, async (c, v) =>
            {
                Control.TemplateContent = new ProfilePage(Control).Content;
                b.IsEnabled = true;
            }));
        }

        
	}
}