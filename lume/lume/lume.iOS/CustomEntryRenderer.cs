using System;
using CoreGraphics;
using lume.CustomObj;
using lume.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LumEntry), typeof(CustomEntryRenderer))]
namespace lume.iOS.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control == null) return;

            LumEntry customEntry = (LumEntry)e.NewElement;
            if (customEntry != null)
            {
                var view = customEntry;

                Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));
                Control.LeftViewMode = UITextFieldViewMode.Always;

                Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
                Control.ReturnKeyType = UIReturnKeyType.Done;
                // Radius for the curves  
                Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);
                // Thickness of the Border Color  
                Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                // Thickness of the Border Width  
                Control.Layer.BorderWidth = view.BorderWidth;
                Control.ClipsToBounds = true;
            }
        }

        private void BorderSet(UITextField Control, LumEntry customEntry)
        {
            Control.Layer.MasksToBounds = true;
            Control.Layer.CornerRadius = customEntry.CornerRadius;
            Control.Layer.BorderWidth = customEntry.BorderWidth;
            Control.Layer.BorderColor = customEntry.BorderColor.ToCGColor();

            if (!customEntry.CurveIsEnabled)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }

            Control.LeftView = new UIView(new CGRect(0, 0, 10, 0));
            Control.LeftViewMode = UITextFieldViewMode.Always;

        }

        private void TintCustomization(UITextField Control)
        {
            UITextField textField = Control;
            textField.BorderStyle = UITextBorderStyle.None;
        }
    }
}
