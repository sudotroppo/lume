using lume.CustomObj;
using lume.Templates;
using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Windows.Input;
using Xamarin.Forms;

namespace lume.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = (MainViewModel)Application.Current.Resources["mainVM"];
        }


        public async void OnClikedButton(object sender, EventArgs e)
        {
            try
            {
                (BindingContext as MainViewModel).SetUtente(Username.Text);
                await Navigation.PushAsync(new MainPage(), false);
            }
            catch (JsonException)
            {
                Username.TextColor = Color.Firebrick;
            }

        }
    }
}
