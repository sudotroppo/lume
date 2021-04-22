using lume.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace lume
{
    //classe per la navigazione tra pagine all'interno di una NavigationPage
    public class Navigator
    {

        private MainPageTemplate mainPageTemplate;
        private LinkedList<ContentTemplatedView> stackList;

        public Navigator(MainPageTemplate mainPageTemplate)
        {
            this.mainPageTemplate = mainPageTemplate;
            stackList = new LinkedList<ContentTemplatedView>();
        }

        private void SetValues()
        {
            mainPageTemplate.CurrentTab = stackList.First();
        }

        public Task PushAsync(ContentTemplatedView cv)
        {
            if (!cv.GetType().Equals(mainPageTemplate.CurrentTab.GetType()))
            {
                if(stackList.Count > 2) 
                {
                    stackList.RemoveLast();
                }
                return Task.Run(() => 
                {
                    stackList.AddFirst(cv);
                    SetValues();
                });
            }
            return Task.CompletedTask;
        }

        public Task PopAsync()
        {
            if(stackList.Count > 1)
            {
                return Task.Run(() => 
                {
                    stackList.RemoveFirst();
                });
            }
            return Task.CompletedTask;
        }
    }
}
