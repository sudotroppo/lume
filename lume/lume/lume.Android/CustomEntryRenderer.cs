using Android.Content;
using Android.Graphics.Drawables;
using lume.CustomObj;
using lume.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LumEntry), typeof(CustomEntryRenderer))]
namespace lume.Droid.Renderers
{
    class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            if (e.NewElement is LumEntry customEntry) BorderSet(customEntry);
        }

        private void BorderSet(LumEntry customEntry)
        {
            var gradientDrawable = new GradientDrawable();

            gradientDrawable.SetCornerRadius(customEntry.CornerRadius * 2);
            gradientDrawable.SetStroke(customEntry.BorderWidth, customEntry.BorderColor.ToAndroid());


            Control.SetBackground(gradientDrawable);
            Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
        }
    }
}
