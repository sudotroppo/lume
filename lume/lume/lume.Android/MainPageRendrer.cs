using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using lume.CustomObj;
using lume.Droid;

[assembly: ExportRenderer(typeof(MainPage), typeof(CustomNavigationPageRenderer))]
namespace lume.Droid.Renderers
{
    public class MainPageRenderer : TabbedRenderer
    {
        public MainPageRenderer(Context context) : base(context)
        {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            Activity activity = Context as Activity;
        }
    }
}