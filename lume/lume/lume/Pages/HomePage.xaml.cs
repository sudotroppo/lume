using lume.Domain;
using lume.Templates;
using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace lume.Pages
{
    public partial class HomePage : ContentTemplatedView
    {

        public ICommand RefreshPage;

        public HomePage()
        {
            InitializeComponent();
            BindingContext = Application.Current.Resources["mainVM"];
            homePostViewer.BindingContext = new HomeViewModel();
        }

        public async void OnRefresh(object sender, EventArgs e)
        {
            refrehView.IsRefreshing = true;
            await Task.Run(() => ((HomeViewModel)homePostViewer.GetValue(BindingContextProperty)).Refresh());
            refrehView.IsRefreshing = false;
        }
    }
}
