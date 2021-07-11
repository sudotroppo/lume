using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.CustomObj
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpRichiesta : ContentView
    {
        public static readonly BindableProperty IsPoppedProperty =
            BindableProperty.Create(
                propertyName: "IsPopped",
                returnType: typeof(bool),
                declaringType: typeof(PopUpRichiesta),
                defaultValue: false,
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: IsPoppedPropertyChanged);

        private static void IsPoppedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (PopUpRichiesta)bindable;
            control.IsPopped = (bool)newValue;
        }

        public bool IsPopped { set; get; }


        public PopUpRichiesta()
        {
            InitializeComponent();
        }
    }
}
