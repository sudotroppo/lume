using lume.CustomObj;
using lume.Domain;
using lume.Pages;
using lume.Utility;
using lume.ViewModels;
using System;
using System.Diagnostics;
using System.Threading;
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

        //20000 = 20s
        public static int requestTimeout = 5000; 

        public App()
        {
            Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });

            InitializeComponent();

            Debug.WriteLine($"{DateTime.Now}");
            _ = GetStorageInfo();
            Debug.WriteLine($"{DateTime.Now}");

            Page initialPage = new LoginPage(email);

            MainPage = new CustomNavigationPage(initialPage);

            Task.Run(() =>
            {
                try
                {
                    DataAccess.RefreshToken();
                    utente = DataAccess.GetUtenteByEmail(email);

                    initialPage = new MainPage();

                    Device.BeginInvokeOnMainThread(() =>
                        (MainPage as CustomNavigationPage).CurrentPage.Navigation.PushAsync(initialPage, false));

                }catch(Exception) { }
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

        public static async Task<bool> GetStorageInfo()
        {
            try
            {
                email = await SecureStorage.GetAsync("email");
                token = await SecureStorage.GetAsync("token");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }

            return true;
        }

        public static async Task<bool> SetUtente(string email, string password)
        {
            try
            {
                TokenResponse tokenResp = DataAccess.GetToken(email, password);
                token = tokenResp.token;
                await SecureStorage.SetAsync("token", token);

                utente = DataAccess.GetUtenteByEmail(email);
                await SecureStorage.SetAsync("email", utente.email);

            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine($"msg = {e}");
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"msg = {e}");
                return false;
            }

            return true;
        }

        public static Task<bool> UpdateUtente()
        {
            try
            {
                utente = DataAccess.GetUtenteByEmail(email);
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine($"msg = {e}");
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
