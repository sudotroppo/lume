using System;
using System.Reflection;
using lume.CustomObj;
using lume.Pages;
using Xamarin.Forms;
using System.Windows.Input;
using lume.Models;

namespace lume.Pages
{
    public partial class LoginPage : ContentPage
    {
        public ICommand SendRequest => new Command<string>(async (url) => await DisplayAlert(url, "Va ancora configurato", "Ok"));

      

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
                await Navigation.PushModalAsync(new TabbedHomePage());
            }
            else
            {
                await DisplayAlert("Attenzione", "Username o password vuoti", "Ok");
            }
        }
    }
}
