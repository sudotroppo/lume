
using lume.CustomObj;
using lume.Pages;
using lume.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;

namespace lume
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });

            InitializeComponent();
            BindingContext = new MainViewModel();
            MainPage = new CustomNavigationPage(new LoginPage());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
