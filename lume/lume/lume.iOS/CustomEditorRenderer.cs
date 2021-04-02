using CoreGraphics;
using lume.CustomObj;
using lume.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LumEditor), typeof(CustomEditorRenderer))]
namespace lume.iOS.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control == null) return;

            LumEditor customEditor = (LumEditor)e.NewElement;
            if (customEditor != null)
            {
                //TintCustomization(Control);
                BorderSet(Control, customEditor);
            }
        }

        private void BorderSet(UITextView Control, LumEditor customEditor)
        {
            Control.Layer.CornerRadius = customEditor.CornerRadius;
            Control.Layer.BorderWidth = customEditor.BorderWidth;
            Control.Layer.BorderColor = customEditor.BorderColor.ToCGColor();

            //Control.LeftView = new UIView(new CGRect(0, 0, 10, 0));
            //Control.LeftViewMode = UITextFieldViewMode.Always;

        }

        private void TintCustomization(UITextField Control)
        {
            UITextField textField = Control;
            textField.BorderStyle = UITextBorderStyle.None;
        }

    }
}
