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

        public HomePage() : base()
        {
            InitializeComponent();
            homePostViewer.BindingContext = new MainViewModel();
        }

        public HomePage(MainPageTemplate Control) : base(Control)
        {
            InitializeComponent();
            homePostViewer.BindingContext = new MainViewModel();
        }

        public async void OnRefresh(object sender, EventArgs e)
        {
            RefreshView r = (RefreshView)sender;
            await Task.Delay(1000);
            r.IsRefreshing = false;
        }
    }
}
