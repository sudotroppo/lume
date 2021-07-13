using System;
using System.Diagnostics;
using lume.Assets;
using lume.CustomObj;
using lume.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;
using lume.Templates;

namespace lume.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {

        public Command CambiaPasswordCommand { get; private set; }
        public Command<View> RichiesteCommand { get; private set; }
        public Command<View> PartecipazioniCommand { get; private set; }
        public Command LogoutCommand { get; private set; }

        public SettingsViewModel()
        {
            CambiaPasswordCommand = new Command(OnCambiaPassword);
            RichiesteCommand = new Command<View>(OnRichieste);
            PartecipazioniCommand = new Command<View>(OnPartecipazioni);
            LogoutCommand = new Command(OnLogout);


        }

        //TODO: implementare i metodi qua sotto

        public void OnLogout()
        {
            Debug.WriteLine("OnLogout");
            SecureStorage.SetAsync("token", "");

            App.utente = null;

            var navPage = Application.Current.MainPage as CustomNavigationPage;

            //navPage.Navigation.InsertPageBefore(new LoginPage(), navPage.CurrentPage);
            navPage.CurrentPage.Navigation.PopAsync();
        }

        public void OnCambiaPassword()
        {
            Debug.WriteLine("OnCambiaPassword");
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
            (navPage.CurrentPage as MainPage).navigator.InsetPageIntoTabIndex(cv,2);
        }
    }
}
