using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
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

        [Obsolete]
        private void BorderSet(LumEntry customEntry)
        {

            var gradientDrawable = new GradientDrawable();

            gradientDrawable.SetCornerRadius(customEntry.CornerRadius * 2);
            gradientDrawable.SetStroke(customEntry.BorderWidth, customEntry.BorderColor.ToAndroid());
            //// creating gradient drawable for the curved background  
            //var _gradientBackground = new GradientDrawable();
            //_gradientBackground.SetShape(ShapeType.Rectangle);
            //_gradientBackground.SetColor(customEntry.BackgroundColor.ToAndroid());

            //// Thickness of the stroke line  
            //_gradientBackground.SetStroke(customEntry.BorderWidth, customEntry.BorderColor.ToAndroid());

            //// Radius for the curves  
            //_gradientBackground.SetCornerRadius(
            //    DpToPixels(this.Context, Convert.ToSingle(customEntry.CornerRadius)));

            //// set the background of the   
            //Control.SetBackground(_gradientBackground);
            //Control.Background.Bounds = _gradientBackground.Bounds;


            // Control.SetBackground(gradientDrawable);
            Control.Background = this.Resources.GetDrawable(Resource.Drawable.customEntry);
            Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);

        }

    }
}
