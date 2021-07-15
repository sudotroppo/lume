using System;
using System.Diagnostics;
using lume.Assets;
using lume.CustomObj;
using lume.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;
using lume.Templates;
using System.Windows.Input;

namespace lume.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {

        private bool _isCambiaPasswordPopped = false;
        public bool IsCambiaPasswordPopped { get => _isCambiaPasswordPopped; set => SetProperty(ref _isCambiaPasswordPopped, value); }

        private bool _isEliminaUtentePopped = false;
        public bool IsEliminaUtentePopped { get => _isEliminaUtentePopped; set => SetProperty(ref _isEliminaUtentePopped, value); }

        public ICommand RichiesteCommand { get; private set; }
        public ICommand PartecipazioniCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public ICommand RichiestaEliminazioneUtenteCommand { get; private set; }
        public ICommand EliminaAccountCommand { get; private set; }
        public ICommand RichiediCambioPasswordCommand { get; private set; }

        public SettingsViewModel()
        {
            RichiesteCommand = new Command<View>(OnRichieste);
            PartecipazioniCommand = new Command<View>(OnPartecipazioni);
            LogoutCommand = new Command(OnLogout);
            RichiestaEliminazioneUtenteCommand = new Command(RichiediEliminazione);
            RichiediCambioPasswordCommand = new Command(RichiediCambioPassword);
        }

        public void RichiediEliminazione()
        {
            IsEliminaUtentePopped = true;
        }


        public void RichiediCambioPassword()
        {
            IsCambiaPasswordPopped = true;
        }


        public void OnLogout()
        {
            Debug.WriteLine("OnLogout");
            SecureStorage.SetAsync("token", "");
            SecureStorage.SetAsync("email", "");

            App.utente = null;

            var navPage = Application.Current.MainPage as CustomNavigationPage;

            //navPage.Navigation.InsertPageBefore(new LoginPage(), navPage.CurrentPage);
            navPage.CurrentPage.Navigation.PopAsync();
        }


        public void OnRichieste(View v)
        {
            Debug.WriteLine("OnRichieste");
            SlideToRight(v, new MyRequestsPage());

        }

        public void OnPartecipazioni(View v)
        {
            Debug.WriteLine("OnPartecipazioni");
            SlideToRight(v, new MyPartecipationsPage());
        }

        private async void SlideToRight(View v, ContentTemplatedView cv)
        {
            await v.TranslateTo(v.Width, 0, 300, Easing.CubicInOut);

            var navPage = Application.Current.MainPage as CustomNavigationPage;
            var navigator = (navPage.CurrentPage as MainPage).navigator;

            cv.navigator = navigator;
            navigator.InsetPageIntoTabIndex(cv,2);
        }
    }
}
