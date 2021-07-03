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
            var vm = Application.Current.Resources["mainVM"] as MainViewModel;

            try
            {
                await Task.Run(() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        activity.IsRunning = true;
                    });

                    vm.SetUtente(Username.Text?.Trim());


                });

                activity.IsRunning = false;
                await Navigation.PushAsync(new MainPage(), false);
            }
            catch (JsonException)
            {
                activity.IsRunning = false;
                Username.TextColor = Color.Firebrick;
            }


        }
    }
}
