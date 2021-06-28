
using lume.CustomObj;
using lume.Pages;
using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace lume
{
    public partial class App : Application
    {

        public static string token;
        private string _user;
        public App()
        {
            Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });

            InitializeComponent();
            BindingContext = new MainViewModel();
            MainPage = new CustomNavigationPage(new LoginPage());

            /*_ = GetUser();
            MainPage = _user != null ? new NavigationPage(new MainPage()) : new NavigationPage(new LoginPage());*/

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

        private async Task GetUser()
        {
            try
            {
                _user = await SecureStorage.GetAsync("username");
                token = await SecureStorage.GetAsync("token");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
