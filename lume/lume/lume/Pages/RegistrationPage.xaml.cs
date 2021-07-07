using Amazon.CognitoIdentityProvider.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using lume.Utility;
using Amazon.CognitoIdentityProvider;
using Amazon;
using lume.Domain;
using System.Threading.Tasks;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {

        public RegistrationPage()
        {
            InitializeComponent();
        }

        public async void SignUp(object sender, EventArgs e)
        {
            Utente u = new Utente()
            {
                nome = Nome_reg.Text?.Trim(),
                cognome = Cognome_reg.Text?.Trim(),
                password = Password_reg.Text?.Trim(),
                email = Email_reg.Text?.Trim(),
                telefono = Telefono_reg.Text?.Trim()
            };

            await Task.Run(() => DataAccess.NewUtente(u));

            await Navigation.PopAsync(false);
        }

        public async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync(false);
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}