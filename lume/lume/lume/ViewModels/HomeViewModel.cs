using System;
using System.Collections.Generic;
using System.ComponentModel;
using lume.Domain;

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


        public HomeViewModel()
        {
            _Posts = portale.getAllRichieste();
        }
    }
}
