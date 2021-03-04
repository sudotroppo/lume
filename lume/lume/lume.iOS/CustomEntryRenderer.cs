using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using lume;
using System.ComponentModel;
using lume.iOS.Renderers;
using CoreGraphics;

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
                TintCustomization(Control, customEntry);
                BorderSet(Control, customEntry);
            }
        }

        private void BorderSet(UITextField Control, LumEntry customEntry)
        {
            Control.Layer.CornerRadius = customEntry.CornerRadius;
            Control.Layer.BorderWidth = customEntry.BorderWidth;
            Control.Layer.BorderColor = customEntry.BorderColor.ToCGColor();

            Control.LeftView = new UIKit.UIView(new CGRect(0, 0, 10, 0));
            Control.LeftViewMode = UIKit.UITextFieldViewMode.Always;

        }

        private void TintCustomization(UITextField Control, LumEntry customEntry)
        {
            UITextField textField = Control;
            textField.BorderStyle = UITextBorderStyle.None;
            textField.TintColor = customEntry.HandleColor.ToUIColor();
        }
    }
}
