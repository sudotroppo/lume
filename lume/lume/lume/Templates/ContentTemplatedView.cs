using System;
using lume.Templates;
using Xamarin.Forms;

namespace lume.Templates
{
    public class ContentTemplatedView : ContentPage
    {
        public MainPageTemplate Control;

        public ContentTemplatedView(MainPageTemplate Control)
        {
            
            this.Control = Control;
        }

        public ContentTemplatedView() : this(null)
        {

        }
    }
}

