using Amazon.CognitoIdentityProvider;
using lume.CustomObj;
using lume.Templates;
using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using lume.Utility;
using lume.Assets;
using lume.Domain;
using Xamarin.Essentials;
using System.Diagnostics;

namespace lume.Pages
{
    public partial class LoginPage : ContentPage
    {
        public string _Email { set;  get; }

        public LoginPage()
        {
            InitializeComponent();

            _Email = SecureStorage.GetAsync("email").Result;

            BindingContext = this;
        }


        public async void OnClikedButton(object sender, EventArgs e)
        {
            activity.IsRunning = true;
            string email = Email.Text?.Trim();
            string password = Password.Text;

            bool condUName = "".Equals(Email.Text) || Email.Text == null;
            bool condPwd = "".Equals(Password.Text) || Password.Text == null;

            if (condUName || condPwd)
            {
                _ = condUName ? Animations.ShakeAnimate(Email) : null;
                _ = condPwd   ? Animations.ShakeAnimate(Password) : null;

                await Application.Current.MainPage.DisplayAlert("Password o Email mancanti", "inserire dei valori", "ok");
                activity.IsRunning = false;

            }
            else
            {
                bool result = true;
                try
                {
                    await Task.Run(async () =>
                    {
                        result = await App.SetUtente(email, password);
                        
                    });



                    if (result)
                    {
                        await App.GetStorageInfo();

                        activity.IsRunning = false;
                        await Navigation.PushAsync(new MainPage(), false);
                    }
                    else
                    {
                        activity.IsRunning = false;
                        _ = Animations.ShakeAnimate(Email);
                        _ = Animations.ShakeAnimate(Password);

                        await Application.Current.MainPage.DisplayAlert("Errore di autenticazione", "Password o email errate", "ok");
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    activity.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert("Errore di autenticazione", "Password o email errate", "ok");

                    _ = Animations.ShakeAnimate(Email);
                    _ = Animations.ShakeAnimate(Password);
                }

            }

        }
    }
}
