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

        public static Utente utente;


        public App()
        {
            Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });

            InitializeComponent();

            _ = GetUtente();


            Page initalPage = new CustomNavigationPage(new LoginPage(email));
            MainPage = initalPage;

            Task.Run(() =>
            {
                try
                {
                    DataAccess.RefreshToken();
                    initalPage = new MainPage();

                }
                catch (UnauthorizedAccessException)
                {
                    initalPage = new LoginPage(email);

                }

                MainPage.Navigation.PushAsync(initalPage, false);
            });



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

                utente = DataAccess.GetUtenteByEmail(email);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public static async Task<bool> SetUtente(string email, string password)
        {
            bool result = true;

            try
            {
                TokenResponse tokenResp = DataAccess.GetToken(email, password);

                await SecureStorage.SetAsync("email", tokenResp.email);
                await SecureStorage.SetAsync("token", tokenResp.token);

                token = tokenResp.token;

                utente = DataAccess.GetUtenteByEmail(email);
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine($"msg = {e}");
                result = false;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"msg = {e}");
                result = false;
            }

            return result;
        }
    }
}
