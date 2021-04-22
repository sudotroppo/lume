using System;
using lume.Templates;
using Xamarin.Forms;

namespace lume.Templates
{
    public class ContentTemplatedView : ContentPage
    {
        public MainPageTemplate Control;

        public Navigator navigator;

        public ContentTemplatedView(Navigator navigator)
        {
            
            this.navigator = navigator;
        }

        public ContentTemplatedView() : this(null)
        {

        }
    }
}

