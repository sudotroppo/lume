using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using lume.CustomObj;
using lume.iOS;
using Plugin.SharedTransitions.Platforms.iOS;
using lume.iOS.Renderers;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace lume.iOS.Renderers
{
    public class CustomNavigationPageRenderer : SharedTransitionNavigationRenderer
    {
        public CustomNavigationPageRenderer()
        {

        }

        

    }
}