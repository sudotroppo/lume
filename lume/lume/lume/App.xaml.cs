
using lume.CustomObj;
using lume.Domain;
using lume.Pages;
using lume.Utility;
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

        public static string email;

        public static Utente utenteCorrente { set; get; }

        public App()
        {
            Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });

            InitializeComponent();

            _ = GetUser();

            MainPage = email != null ? new CustomNavigationPage(new MainPage()) : new CustomNavigationPage(new LoginPage());
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
                email = await SecureStorage.GetAsync("email");
                token = await SecureStorage.GetAsync("token");

                utenteCorrente = DataAccess.GetUtenteByEmail(email);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
