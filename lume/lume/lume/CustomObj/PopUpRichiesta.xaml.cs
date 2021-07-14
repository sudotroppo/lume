using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.CustomObj
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpRichiesta : ContentView
    {
        public static readonly BindableProperty IsPoppedProperty =
            BindableProperty.Create(
                propertyName: nameof(IsPopped),
                returnType: typeof(bool),
                declaringType: typeof(PopUpRichiesta),
                defaultValue: false,
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: IsPoppedPropertyChanged);

        private static void IsPoppedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (PopUpRichiesta)bindable;
            var value = (bool)newValue;

            if (value)
            {
                Debug.WriteLine($"AnimatePopUp convert to True");
                control.TranslateTo(0, 0.35 * control.Height, 500, Easing.CubicInOut);
            }
            else
            {
                Debug.WriteLine($"AnimatePopUp convert to False, Heigth = {control.Height}");
                control.TranslateTo(0, control.Height, 500, Easing.CubicInOut);
            }
            control.IsPopped = value;

        }

        public bool IsPopped { set => SetValue(IsPoppedProperty, value); get => (bool)GetValue(IsPoppedProperty); }


        public PopUpRichiesta()
        {
            InitializeComponent();
        }
    }
}
