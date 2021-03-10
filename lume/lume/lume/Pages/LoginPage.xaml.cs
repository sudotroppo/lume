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



        public async void OnClikedButton(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TabbedHomePage(), false);
        }
    }
}
