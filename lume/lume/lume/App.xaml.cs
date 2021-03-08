using System;
using System.Threading.Tasks;
using lume.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });
            InitializeComponent();

            MainPage = new FillRequestPage();

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
