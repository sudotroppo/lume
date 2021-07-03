using lume.Services.Cognito;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        public async void OnClikedButton(object sender, EventArgs e)
        {
            CognitoUser u = new CognitoUser();
            u.Email = Email_reg.Text;
            u.Password = Password_reg.Text;
            CognitoUserStore us = new CognitoUserStore();
            await us.CreateAsync(u);
            await Navigation.PopModalAsync();
        }
    }
}