using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using lume.Domain;
using Xamarin.Forms;

namespace lume.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private List<Richiesta> _Posts;

        PortaleLume portale = PortaleLume.getIstance();


        public List<Richiesta> Posts
        {
            get { return _Posts; }

            set
            {
                _Posts = value;
                OnPropertyChanged();
            }
        }

        public void Refresh()
        {
            _Posts = portale.getAllRichieste();
        }

        public HomeViewModel()
        {
            _Posts = portale.getAllRichieste();
        }
    }
}
