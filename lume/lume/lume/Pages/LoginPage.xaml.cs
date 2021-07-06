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

        public LoginPage() : this(null) { }

        public LoginPage(string email)
        {
            InitializeComponent();

            _Email = email;

            BindingContext = this;
        }

        public async void OnClikedButton(object sender, EventArgs e)
        {
            string email = Email.Text?.Trim();
            string password = Password.Text;


            bool condUName = "".Equals(Email.Text) || Email.Text == null;
            bool condPwd = "".Equals(Password.Text) || Password.Text == null;

            Email.IsEnabled = false;
            Password.IsEnabled = false;

            if (condUName || condPwd)
            {
                _ = condUName ? Animations.ShakeAnimate(Email) : null;
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

                        await App.SetToken(email, password);

                    });

                    activity.IsRunning = false;

                    await Navigation.PushAsync(new MainPage(), false);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    activity.IsRunning = false;

                    _ = Animations.ShakeAnimate(Email);
                    _ = Animations.ShakeAnimate(Password);
                }

            }
            Email.IsEnabled = true;
            Password.IsEnabled = true;

        }
    }
}
