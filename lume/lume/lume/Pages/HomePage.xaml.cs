using lume.Domain;
using lume.Templates;
using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace lume.Pages
{
    public partial class HomePage : ContentTemplatedView
    {
        public HomePage()
        {
            InitializeComponent();
            homePostViewer.BindingContext = new HomeViewModel();
        }

        public async void OnRefresh(object sender, EventArgs e)
        {
            RefreshView r = (RefreshView)sender;
            await Task.Run(() =>
            {
                homePostViewer.BindingContext = new HomeViewModel();
            });

            r.IsRefreshing = false;
        }
    }
}
