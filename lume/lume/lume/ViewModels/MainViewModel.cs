using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using lume.Domain;
using Xamarin.Forms;
using lume.Utility;

namespace lume.ViewModels
{
    
    public class MainViewModel : BaseViewModel
    {

        protected PortaleLume portaleLume = PortaleLume.getIstance();

        Utente _CurrentUser;

        public Utente CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            if(portaleLume.utenteCorrente == null)
            {
                _CurrentUser = DataAccess.GetUtenteByEmail("dav.galletti@stud.uniroma3.it");
                portaleLume.utenteCorrente = CurrentUser;
            }
            else
            {
                CurrentUser = portaleLume.utenteCorrente;
            }


        }
    }
}
