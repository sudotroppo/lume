using Npgsql;
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

            var connectionString = "";
            var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            /*metti nel database cryptando password*/
            using (var cmd = new NpgsqlCommand("INSERT INTO public.\"utente\" VALUES (@email,@nome,@cognome,@password,@username,@cod_fiscale)"
                , conn))
            {
                cmd.Parameters.AddWithValue("email", Email_reg.Text);
                cmd.Parameters.AddWithValue("nome", Nome_reg.Text);
                cmd.Parameters.AddWithValue("cognome", Cognome_reg.Text);
                cmd.Parameters.AddWithValue("password", Password_reg.Text);
                cmd.Parameters.AddWithValue("username", Username_reg.Text);
                cmd.Parameters.AddWithValue("cod_fiscale", CodiceFiscale.Text);
                cmd.ExecuteNonQuery();
            }
            await conn.CloseAsync();
            

            await DisplayAlert("Ottimo", "I tuoi dati verranno esaminati, riceverai un'email di conferma!", "Ok");
            await Navigation.PopModalAsync();
        }
    }
}