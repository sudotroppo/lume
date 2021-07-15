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
using lume.CustomObj;

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
            var nav = (Application.Current.MainPage as CustomNavigationPage);

            try
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
                    _ = condEmail ? Animations.ShakeAnimate(Email_reg) : null;
                    _ = condCitta ? Animations.ShakeAnimate(Citta_reg) : null;
                    _ = condPassword ? Animations.ShakeAnimate(Password_reg) : null;

                    return;
                }

                if (DataAccess.ExistsUtente(Email_reg.Text.Trim()))
                {
                    await nav.CurrentPage.DisplayAlert("Email!", "errore, questa mail è già in uso", "ok");
                    return;
                }


                Utente u = new Utente()
                {
                    nome = Nome_reg.Text.Trim(),
                    cognome = Cognome_reg.Text.Trim(),
                    password = Password_reg.Text.Trim(),
                    email = Email_reg.Text.Trim(),
                    telefono = Telefono_reg.Text?.Trim(),
                    citta = Citta_reg.Text.Trim()
                };


                await Task.Run(() => DataAccess.NewUtente(u));

                await nav.DisplayAlert("Congratulazioni", "registrazione avvenuta con successo", "ok");
                await Navigation.PopAsync(false);
            }
            catch(Exception)
            {

                await nav.DisplayAlert("Errore", "C'è stato un errore di connessione, si prega di riprovare più tardi", "ok");

            }

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