using lume.Pages;
using lume.Templates;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            if(index >= mainPageTemplate.Tabs.Capacity) { throw new IndexOutOfRangeException("tab index non esistente"); }

            mainPageTemplate.Tabs[index] = cv;
            mainPageTemplate.TabChanged();

        }

        public void GoTo(int index)
        {
            mainPageTemplate.CurrentTab = index;
            mainPageTemplate.TabChanged();
        }

    }
}
