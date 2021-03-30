using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace lume.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async void OnRefresh(object sender, EventArgs e)
        {
            RefreshView r = (RefreshView)sender;
            await Task.Delay(1000);
            r.IsRefreshing = false;
        }
    }
}
