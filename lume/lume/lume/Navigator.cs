using lume.Pages;
using lume.Templates;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace lume
{
    //classe per la navigazione tra Tab di una MainPage
    public class Navigator
    {

        private MainPage mainPageTemplate;

        public Navigator(MainPage mainPage)
        {
            mainPageTemplate = mainPage;
        }

        public void InsetPageIntoTabIndex(ContentTemplatedView cv, int index)
        {
            if (index >= mainPageTemplate.Tabs.Capacity) { throw new IndexOutOfRangeException("tab index non esistente"); }

            mainPageTemplate.Tabs[index] = cv;
            mainPageTemplate.TabChanged();

        }

        public void GoTo(int index)
        {
            mainPageTemplate.CurrentTab = index;
            mainPageTemplate.TabChanged();
        }

        public void Alert(string msg,
            string confermaNome, ICommand confermaCommand, object confermaParameter,
            string annullaNome, ICommand annullaCommand, object annullaParameter)
        {
            mainPageTemplate.Alert(msg, confermaNome, confermaCommand, confermaParameter, annullaNome, annullaCommand, annullaParameter);
        }

        public void Alert(string msg, string confermaNome, string annullaNome)
        {
            mainPageTemplate.Alert(msg, confermaNome, null, null, annullaNome, null, null);
        }

        public void Alert(string msg, string confermaNome, ICommand confermaCommand, string annullaNome, ICommand annullaCommand)
        {
            mainPageTemplate.Alert(msg, confermaNome, confermaCommand, null, annullaNome, annullaCommand, null);
        }
    }
}
