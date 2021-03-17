using System;
using Android.Content;
using lume.Droid.Renderes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace lume.Droid.Renderes
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        [Obsolete]
        public CustomTabbedPageRenderer(Context context)
        {

        }
    }
}
