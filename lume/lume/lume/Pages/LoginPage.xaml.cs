using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace lume.Pages
{
    public partial class LoginPage : ContentPage
    {
        public ICommand SendRequest => new Command<string>(async (url) => await DisplayAlert(url, "Va ancora configurato", "Ok"));

        public LoginPage()
        {
            InitializeComponent();
        }


        public async void OnClikedButton(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TabbedHomePage());

        }
    }
}
