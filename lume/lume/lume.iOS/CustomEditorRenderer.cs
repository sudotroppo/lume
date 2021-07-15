using System;
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
                var view = customEditor;

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
