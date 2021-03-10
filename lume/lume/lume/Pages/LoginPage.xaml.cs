using System;
using lume.Models;
using lume.Pages;
using Xamarin.Forms;

namespace lume
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

        }

        async void OnClikedButton(object o, EventArgs e)
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
