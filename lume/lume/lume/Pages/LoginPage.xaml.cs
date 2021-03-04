using System;
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

        public async void OnClikedButton(object o, EventArgs e)
        {
            Button b = (Button)o;
            await Navigation.PushModalAsync(new HomePage());
        }
    }
}
