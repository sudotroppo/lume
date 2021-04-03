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
                //TintCustomization(Control, customEntry);
                BorderSet(Control, customEntry);
            }
        }

        private void BorderSet(UITextField Control, LumEntry customEntry)
        {
            Control.Layer.CornerRadius = customEntry.CornerRadius;
            Control.Layer.BorderWidth = customEntry.BorderWidth;
            Control.Layer.BorderColor = customEntry.BorderColor.ToCGColor();

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
