using lume.Pages;
using lume.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace lume
{
    //classe per la navigazione tra Tab di una MainPage
    public class Navigator
    {

        private MainPage mainPageTemplate;
        private LinkedList<ContentTemplatedView> stackList;

        public Navigator(MainPage mainPage)
        {
            this.mainPageTemplate = mainPage;
            stackList = new LinkedList<ContentTemplatedView>();
        }

        private void SetValues()
        {
            mainPageTemplate.CurrentTab = stackList.First();
        }

        public void PushAsync(ContentTemplatedView cv)
        {
            if (!cv.GetType().Equals(mainPageTemplate.CurrentTab.GetType()))
            {
                if(stackList.Count > 3) 
                {
                    stackList.RemoveLast();
                }
                stackList.AddFirst(cv);
                SetValues();
            }
        }

        public bool PopAsync()
        {
            if(stackList.Count > 1)
            {
                stackList.RemoveFirst();
                SetValues();
            }
            return true;
        }
    }
}
