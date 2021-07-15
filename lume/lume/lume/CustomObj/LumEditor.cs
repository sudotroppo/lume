using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace lume.CustomObj
{


    public class LumEditor : Editor
    {

        #region Bindable Properties
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(LumEditor));

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(LumEditor));

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(LumEditor));

        public static readonly BindableProperty CurveIsEnabledProperty = BindableProperty.Create(nameof(CurveIsEnabled), typeof(bool), typeof(LumEditor));
        #endregion

        #region Properties

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        //raggio di approccio degli spigoli
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public int BorderWidth
        {
            get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public bool CurveIsEnabled
        {
            get => (bool)GetValue(CurveIsEnabledProperty);
            set => SetValue(CurveIsEnabledProperty, value);
        }
        #endregion

        public LumEditor()
        {
        }
    }
}
