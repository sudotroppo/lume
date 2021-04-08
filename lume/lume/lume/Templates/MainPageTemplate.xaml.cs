using lume.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageTemplate : ContentPage
    {
        public MainPageTemplate()
        {
            InitializeComponent();
        }
        public async void OnProfileClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotificationsPage(), false);
        }

        public async void OnNewRequestClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FillRequestPage(), false);
        }
        public async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage(), false);
        }
    }
}