using System;
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

            await Navigation.PushModalAsync(new TabbedHomePage());
           
        }
    }
}
