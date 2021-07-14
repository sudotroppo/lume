using System;
using System.Threading.Tasks;
using lume.Assets;
using lume.Templates;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace lume.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class SettingsPage : ContentTemplatedView    {        public SettingsPage(Navigator navigator) : base(navigator)        {            InitializeComponent();        }

        public async void OnBackButtonCliked(object sender, EventArgs e)
        {
            Button b = (sender as Button);
            b.IsEnabled = false;

            Animation ToTheProfilePage = new Animation  // animazione di cambio pagina
            {
                {0, 1, Animations.SlideOfX(ToTheLeft, -50, Easing.CubicInOut) },
                {0, 1, Animations.SlideOf(Settings, -35, 25, Easing.CubicInOut) },
                {0, 1, Animations.ScaleTo(Settings, 0.2, 0.2, Easing.CubicInOut) },
            };

            await Task.Run(() => ToTheProfilePage.Commit(this, "ToTheProfilePage", 1, 500, Easing.Linear, (c, v) =>
            {
                navigator.InsetPageIntoTabIndex(new ProfilePage(navigator), 2);
                b.IsEnabled = true;
            }));
        }
    }}