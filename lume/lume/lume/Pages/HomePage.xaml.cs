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

        public static long offset = 5;
        public readonly long step = 4;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            homePostViewer.BindingContext = new HomeViewModel();
        }

        public async void OnRefresh(object sender, EventArgs e)
        {
            refrehView.IsRefreshing = true;
            await Task.Run(() => ((HomeViewModel)homePostViewer.BindingContext).Refresh());
            refrehView.IsRefreshing = false;
        }

        public void OnScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            Console.WriteLine($"------VericalDelta={e.VerticalDelta}, VerticalOffset={e.VerticalOffset}, LastVisibleIndex={e.LastVisibleItemIndex}------");

            //var s = sender as CollectionView;

            //if(e.LastVisibleItemIndex > (s.ItemsSource as List<Richiesta>).Count - 4)
            //{
            //    var vm = homePostViewer.BindingContext as HomeViewModel;


            //    Task.Run(() =>
            //    {
            //        vm.IncrementPosts(offset, step);
            //        offset += step;
            //    });
            //}
        }
    }
}
