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


            //var ts = new CancellationTokenSource();
            //CancellationToken ct = ts.Token;

            //bool authorized = true;

            //Debug.WriteLine("Start a new Thread");
            //Task.Factory.StartNew(() =>
            //{
            //    try
            //    {
            //        var t1 = Task.Factory.StartNew(() =>
            //        {

            //            Debug.WriteLine("send request");
            //            DataAccess.RefreshToken();
            //            utente = DataAccess.GetUtenteByEmail(email);
            //            Debug.WriteLine("response handled");
            //        });

            //        while (!t1.IsCompleted)
            //        {
            //            Debug.WriteLine("waiting");

            //            if (ct.IsCancellationRequested)
            //            {
            //                Debug.WriteLine("cancelled");
            //                t1.Dispose();
            //            }
            //        }


            //    }
            //    catch (UnauthorizedAccessException)
            //    {

            //        authorized = false;
            //    }
            //    finally
            //    {
            //        Device.BeginInvokeOnMainThread(async () =>
            //        {
            //            Debug.WriteLine("set page");

            //            MainPage
            //                .Navigation.InsertPageBefore
            //                    (ts.IsCancellationRequested || !authorized ?
            //                    (Page)new LoginPage(email) :
            //                    (Page)new HomePage(), (MainPage as CustomNavigationPage).CurrentPage);

            //            await (MainPage as CustomNavigationPage).CurrentPage.Navigation.PopAsync();

            //            await (MainPage as CustomNavigationPage).CurrentPage.DisplayAlert("", "", "ok");
            //        });
            //    }

            //}, ct);

            //Task.Factory.StartNew(() =>
            //{
            //    Debug.WriteLine("inizio timer");
            //    ts.CancelAfter(timeout);
            //    Debug.WriteLine("fine timer");

            //});
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static async Task GetStorageInfo()
        {
            try
            {
                email = await SecureStorage.GetAsync("email");
                token = await SecureStorage.GetAsync("token");
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
                Domain.TokenResponse tokenResp = DataAccess.GetToken(email, password);

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
