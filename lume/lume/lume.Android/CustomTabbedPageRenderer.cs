using System;
using lume.Droid.Renderes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace lume.Droid.Renderes
{
    [Obsolete]
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
    }
}
