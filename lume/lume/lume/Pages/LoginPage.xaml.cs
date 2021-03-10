using System;
using System.Reflection;
using lume.Pages;
using Xamarin.Forms;
using System.Windows.Input;
using lume.Models;

namespace lume
{
    public partial class LoginPage : ContentPage
    {
        public ICommand TapCommand => new Command(() => Navigation.PushModalAsync(new RegistrationPage(), true));

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = this;
        }


        public async void OnClikedButton(object sender, EventArgs e)
        {
            User user = new User(Username.Text, Password.Text);

            if (user.CheckInformation())
            {
                await DisplayAlert("Benvenuto", "Login effettuato.", "Ok");
                await Navigation.PushModalAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Attenzione", "Username o password vuoti", "Ok");
            }
        }
    }
}
