using System;
using System.Collections.Generic;
using Plugin.SharedTransitions;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace lume.CustomObj
{
    public class CustomNavigationPage : SharedTransitionNavigationPage
    {
        public CustomNavigationPage(Xamarin.Forms.Page root) : base(root)
        {
            //Code to make the NavigationPage translucent, if you don't want that you can remove it
            On<iOS>().SetIsNavigationBarTranslucent(true);
            BarBackgroundColor = Color.Transparent;
            BarTextColor = Color.Black;
            On<iOS>().SetHideNavigationBarSeparator(true);
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}
