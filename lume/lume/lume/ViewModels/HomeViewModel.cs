using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using lume.Domain;
using lume.Utility;
using Xamarin.Forms;

namespace lume.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private bool _IsRefreshing;
        public bool IsRefreshing
        {
            get { return _IsRefreshing; }

            set
            {
                _IsRefreshing = value;
                OnPropertyChanged();
            }
        }


        public ICommand RefreshPage => new Command(async () =>
        {
            Debug.WriteLine($"{IsRefreshing}");

            await Task.Run(() => Posts = new ObservableCollection<Richiesta>(DataAccess.GetAllRichieste()));

            IsRefreshing = false;
            Debug.WriteLine($"{IsRefreshing}");
        });

        private ObservableCollection<Richiesta> _Posts;

        public ObservableCollection<Richiesta> Posts
        {
            get { return _Posts; }

            set
            {
                _Posts = value;
                OnPropertyChanged();
            }
        }


        public HomeViewModel()
        {
            Task.Run(() =>
            {
                Posts = new ObservableCollection<Richiesta> (DataAccess.GetAllRichieste());
            });
        }
    }
}
