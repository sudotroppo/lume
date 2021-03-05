﻿using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Content;
using lume.Droid.Renderers;
using System;

[assembly: ExportRenderer(typeof(lume.LumEntry), typeof(CustomEntryRenderer))]
namespace lume.Droid.Renderers
{
    class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;
            
            if (e.NewElement is LumEntry customEntry) borderSet(customEntry);
        }

        private void borderSet(LumEntry customEntry)
        {
            var gradientDrawable = new GradientDrawable();

            gradientDrawable.SetCornerRadius(customEntry.CornerRadius * 2);
            gradientDrawable.SetStroke((int)customEntry.BorderWidth, color: customEntry.BorderColor.ToAndroid());

            
            Control.SetBackground(gradientDrawable);
            Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
        }
    }
}
