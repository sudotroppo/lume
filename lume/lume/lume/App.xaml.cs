
using lume.CustomObj;
using lume.Domain;
using lume.Pages;
using lume.Utility;
using lume.ViewModels;
using System;
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

        public static MainViewModel mainVM;


        public App()
        {
            Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });

            InitializeComponent();

            Page initalPage;

            mainVM = new MainViewModel();
            _ = GetUtente();

            try
            {
                DataAccess.RefreshToken();
                initalPage = new CustomNavigationPage(new MainPage());
            }
            catch (UnauthorizedAccessException)
            {
                initalPage = new CustomNavigationPage(new LoginPage(email));
                
            }

            MainPage = initalPage;

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

        public static async Task GetUtente()
        {
            try
            {
                email = await SecureStorage.GetAsync("email");
                token = await SecureStorage.GetAsync("token");

                mainVM.PullUtente(email);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public static async Task SetToken(string email, string password)
        {
            try
            {
                TokenResponse tokenResp = DataAccess.GetToken(email, password);

                await SecureStorage.SetAsync("email", tokenResp.email);
                await SecureStorage.SetAsync("token", tokenResp.token);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
