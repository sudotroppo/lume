using System;
using System.Reflection;
using lume.Pages;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using System.Diagnostics;

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

<<<<<<< HEAD


        public async void OnClikedButton(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TabbedHomePage(), false);
=======
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
>>>>>>> 0d506809470c4754518f6dd9f708e070e72e59b2
        }
    }
}
