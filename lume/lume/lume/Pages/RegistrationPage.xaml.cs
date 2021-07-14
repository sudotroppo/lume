using Amazon.CognitoIdentityProvider.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using lume.Utility;
using Amazon.CognitoIdentityProvider;
using Amazon;
using lume.Domain;
using System.Threading.Tasks;
using lume.Assets;

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

            bool condNome = "".Equals(Nome_reg.Text) || Nome_reg.Text == null;
            bool condCognome = "".Equals(Cognome_reg.Text) || Cognome_reg.Text == null;
            bool condPassword = "".Equals(Password_reg.Text) || Password_reg.Text == null;
            bool condEmail = "".Equals(Email_reg.Text) || Email_reg.Text == null;
            bool condCitta = "".Equals(Citta_reg.Text) || Citta_reg.Text == null;

            if (condNome || condCognome || condEmail || condPassword || condCitta)
            {
                _ = condNome ? Animations.ShakeAnimate(Nome_reg) : null;
                _ = condCognome ? Animations.ShakeAnimate(Cognome_reg) : null;
                _ = condPassword ? Animations.ShakeAnimate(Email_reg) : null;
                _ = condEmail ? Animations.ShakeAnimate(Cognome_reg) : null;
                _ = condCitta ? Animations.ShakeAnimate(Citta_reg) : null;

                return;
            }

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