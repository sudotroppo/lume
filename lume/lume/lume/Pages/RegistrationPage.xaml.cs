using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await DisplayAlert("Ottimo", "I tuoi dati verranno esaminati, riceverai un'email di conferma!", "Ok");
            await Navigation.PopModalAsync();
        }
    }
}