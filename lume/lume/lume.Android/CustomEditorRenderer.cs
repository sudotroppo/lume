using lume.CustomObj;
using lume.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using lume;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using System;

[assembly: ExportRenderer(typeof(LumEditor), typeof(CustomEditorRenderer))]
namespace lume.Droid.Renderers
{
    class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            if (e.NewElement is LumEditor customEditor) BorderSet(customEditor);
        }

        private void BorderSet(LumEditor customEditor)
        {
            var gradientDrawable = new GradientDrawable();

            gradientDrawable.SetCornerRadius(customEditor.CornerRadius * 2);
            gradientDrawable.SetStroke(customEditor.BorderWidth, customEditor.BorderColor.ToAndroid());


            Control.Background = this.Resources.GetDrawable(Resource.Drawable.customEntry);
            Control.SetPadding(50, Control.PaddingTop, 35, Control.PaddingBottom);

        }
    }
}
