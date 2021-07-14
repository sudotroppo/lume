using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using lume.Domain;
using lume.Utility;
using Xamarin.Forms;

namespace lume.ViewModels
{
    public class NotificationViewModel : BaseViewModel
    {

        private bool _isRefreshing = false;
        public bool IsRefreshing { get => _isRefreshing; set => SetProperty(ref _isRefreshing, value); }

        private ObservableCollection<Notifica> _notifiche;
        public ObservableCollection<Notifica> Notifiche { get => _notifiche; set => SetProperty(ref _notifiche, value); }


        public ICommand RefreshCommand { get; private set; }


        public NotificationViewModel()
        {
            RefreshCommand = new Command(OnRefresh);

            Task.Run(() =>
            {
                if (Notifiche == null || Notifiche.Count == 0)
                {
                    Notifiche = new ObservableCollection<Notifica>(DataAccess.GetNotificheByUtente());

                }
            });
            
        }

        public void OnRefresh()
        {
            IsRefreshing = true;
            Task.Run(() =>
            {
                Notifiche = new ObservableCollection<Notifica>(DataAccess.GetNotificheByUtente());

                Device.BeginInvokeOnMainThread(() => IsRefreshing = false);
            });
        }
    }
}
