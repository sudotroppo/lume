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

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        public async void OnClikedButton(object sender, EventArgs e)
        {
            string email = Username.Text;
            string password = Password.Text;


            bool condUName = "".Equals(Username.Text) || Username.Text == null;
            bool condPwd = "".Equals(Password.Text) || Password.Text == null;

            Username.IsEnabled = false;
            Password.IsEnabled = false;

            if (condUName || condPwd)
            {
                _ = condUName ? Animations.ShakeAnimate(Username) : null;
                _ = condPwd   ? Animations.ShakeAnimate(Password) : null;
            }
            else
            {
                try
                {
                    await Task.Run(async () =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            activity.IsRunning = true;
                        });

                        Debug.WriteLine("A");
                        TokenResponse token = DataAccess.GetToken(email, password);
                        Debug.WriteLine("B");

                        
                        Debug.WriteLine($"email = {token.email}, token = {token.token}");

                        await SecureStorage.SetAsync("email", token.email);
                        await SecureStorage.SetAsync("token", token.token);

                        App.utenteCorrente = DataAccess.GetUtenteByEmail(token.email);

                        Debug.WriteLine("C");

                    });

                    activity.IsRunning = false;
                    await Navigation.PushAsync(new MainPage(), false);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    activity.IsRunning = false;
                    _ = Animations.ShakeAnimate(Username);
                    _ = Animations.ShakeAnimate(Password);
                }

            }
            Username.IsEnabled = true;
            Password.IsEnabled = true;

        }
    }
}
