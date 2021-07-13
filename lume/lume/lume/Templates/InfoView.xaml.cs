using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoView : ContentView
    {
        public static readonly BindableProperty FieldNameProperty =
                               BindableProperty.Create(
                                   propertyName: "FieldName",
                                   returnType: typeof(string),
                                   declaringType: typeof(InfoView),
                                   defaultValue: "",
                                   defaultBindingMode: BindingMode.TwoWay,
                                   propertyChanged: FieldNamePropertyChanged);

        public static readonly BindableProperty FieldValueProperty =
                               BindableProperty.Create(
                                   propertyName: "FieldValue",
                                   returnType: typeof(string),
                                   declaringType: typeof(InfoView),
                                   defaultValue: "",
                                   defaultBindingMode: BindingMode.TwoWay,
                                   propertyChanged: FieldValuePropertyChanged);

        public static readonly BindableProperty IsReadOnlyProperty =
                               BindableProperty.Create(
                                   propertyName: "IsReadOnly",
                                   returnType: typeof(bool),
                                   declaringType: typeof(InfoView),
                                   defaultValue: true,
                                   defaultBindingMode: BindingMode.TwoWay,
                                   propertyChanged: IsReadOnlyPropertyChanged);

        public string FieldName
        {
            set => SetValue(FieldNameProperty, value);
            get => (string)GetValue(FieldNameProperty);
        }

        public string FieldValue
        {
            set => SetValue(FieldValueProperty, value);
            get => (string)GetValue(FieldValueProperty);
        }

        public bool IsReadOnly
        {
            set => SetValue(IsReadOnlyProperty, value);
            get => (bool)GetValue(IsReadOnlyProperty);
        }

        private static void IsReadOnlyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (InfoView)bindable;
            control.IsReadOnly = (bool)newValue;
        }

        private static void FieldNamePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (InfoView)bindable;
            control.FieldName = (string)newValue;
        }

        private static void FieldValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (InfoView)bindable;
            control.FieldValue = (string)newValue;
        }

        public InfoView()
        {
            InitializeComponent();
            stack.BindingContext = this;
        }
    }
}