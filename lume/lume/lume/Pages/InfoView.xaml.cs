using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoView : ContentView
    {
        public static readonly BindableProperty FieldNameProperty = BindableProperty.Create(nameof(FieldName), typeof(string), typeof(InfoView), null);
        public static readonly BindableProperty FieldValueProperty = BindableProperty.Create(nameof(FieldValue), typeof(string), typeof(InfoView), null);
        public static readonly BindableProperty EditableProperty = BindableProperty.Create(nameof(IsEditable), typeof(bool), typeof(InfoView), false);

        public string FieldName
        {
            set => SetValue(FieldNameProperty, value);
            get => (String)GetValue(FieldNameProperty);
        }

        public string FieldValue
        {
            set => SetValue(FieldValueProperty, value);
            get => (String)GetValue(FieldValueProperty);
        }

        public bool IsEditable
        {
            set => SetValue(EditableProperty, value);
            get => (bool)GetValue(EditableProperty);
        }

        public InfoView()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}