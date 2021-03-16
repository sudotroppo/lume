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
<<<<<<< HEAD
            User user = new User(Username.Text, Password.Text);

            // Sezione commentata per evitare il popup in fase di sviluppo

            /*
            if (user.CheckInformation())
            {
                await DisplayAlert("Benvenuto", "Login effettuato.", "Ok");
                await Navigation.PushModalAsync(new TabbedHomePage());
            }
            else
            {
                await DisplayAlert("Attenzione", "Username o password vuoti", "Ok");
            }
            */

            // Da eliminare quando la sezione superiore sarà completata
            await Navigation.PushModalAsync(new TabbedHomePage());
=======
            await Navigation.PushModalAsync(new TabbedHomePage());
           
>>>>>>> davide
        }
    }
}
