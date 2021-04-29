using System;
using lume.Assets;
using lume.Pages;
using lume.Templates;
using Xamarin.Forms;

namespace lume.Templates
{
    public abstract class ContentTemplatedView : ContentPage
    {
        public MainPage Control;

        public Navigator navigator;

        private AnimationSupport animationSupport;

        public EventHandler onSettedTab;

        public ContentTemplatedView(Navigator navigator)
        {
            animationSupport = new AnimationSupport();
            this.navigator = navigator;
        }

        public ContentTemplatedView() : this(null)
        {

        }
    }
}

