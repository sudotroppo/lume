using lume.CustomObj;
using lume.Templates;
using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace lume.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = Application.Current.Resources["mainVM"];
        }

        public async void OnClikedButton(object sender, EventArgs e)
        {
            var vm = BindingContext as MainViewModel;


            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    vm.SetLoad(true);
                });

                vm.SetUtente(Username.Text.Trim());


            });

            vm.SetLoad(false);
            await Navigation.PushAsync(new MainPage(), false);



        }
    }
}
