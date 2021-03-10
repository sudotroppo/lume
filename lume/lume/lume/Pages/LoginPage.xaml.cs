using System;
using lume.Pages;
using Xamarin.Forms;
using System.Collections.Generic;

namespace lume
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public async void OnClikedButton(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TabbedHomePage(), false);
        }
    }
}
