using Amazon.CognitoIdentityProvider.Model;
using lume.Services.Cognito;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using lume.Utility;
using Amazon.CognitoIdentityProvider;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {

        private readonly AmazonCognitoIdentityProviderClient _client = new AmazonCognitoIdentityProviderClient();
        private readonly string _clientId = Constants.CLIENT_ID;
        private readonly string _poolId = Constants.USERPOOL_ID;
        public RegistrationPage()
        {

            InitializeComponent();
        }

        public async void OnClikedButton(object sender, EventArgs e)
        {
            try
            {
                SignUpRequest signUpRequest = new SignUpRequest
                {
                    ClientId = _clientId,
                    Password = Password_reg.Text,
                    Username = Email_reg.Text
                };

                AttributeType emailAttribute = new AttributeType()
                {
                    Name = "email",
                    Value = Email_reg.Text
                };
                signUpRequest.UserAttributes.Add(emailAttribute);

                var signUpResult = await _client.SignUpAsync(signUpRequest);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        private async void ConfirmButton_Click(object sender, EventArgs e)
        {
            try
            {
                Amazon.CognitoIdentityProvider.Model.ConfirmSignUpRequest confirmRequest = new ConfirmSignUpRequest()
                {
                    Username = Email_reg.Text,
                    ClientId = _clientId,
                    ConfirmationCode = ConfirmButton.Text
                };

                var confirmResult = await _client.ConfirmSignUpAsync(confirmRequest);
            }
            catch (Exception ex)
            {
                string message = ex.Message;

            }
        }
    }
}