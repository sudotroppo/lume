using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.CustomObj
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpConferma : ContentView
    {

        private static ICommand defaultCommand = new Command((obj) => { });

        public static readonly BindableProperty ConfermaCommandProperty =
            BindableProperty.Create(
                propertyName: nameof(ConfermaCommand),
                returnType: typeof(ICommand),
                declaringType: typeof(PopUpConferma),
                defaultValue: defaultCommand,
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty ConfermaCommandParameterProperty =
            BindableProperty.Create(
                propertyName: nameof(ConfermaCommandParameter),
                returnType: typeof(object),
                declaringType: typeof(PopUpConferma),
                defaultValue: null,
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty AnnullaCommandProperty =
            BindableProperty.Create(
                propertyName: nameof(AnnullaCommand),
                returnType: typeof(ICommand),
                declaringType: typeof(PopUpConferma),
                defaultValue: defaultCommand,
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty AnnullaCommandParameterProperty =
            BindableProperty.Create(
                propertyName: nameof(AnnullaCommandParameter),
                returnType: typeof(object),
                declaringType: typeof(PopUpConferma),
                defaultValue: null,
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty PopUpTextProperty =
           BindableProperty.Create(
               propertyName: nameof(PopUpText),
               returnType: typeof(string),
               declaringType: typeof(PopUpConferma),
               defaultValue: "",
               defaultBindingMode: BindingMode.TwoWay,
               propertyChanged: PopUpTextPropertyChanged);

        public static readonly BindableProperty IsPoppedProperty =
           BindableProperty.Create(
               propertyName: nameof(IsPopped),
               returnType: typeof(bool),
               declaringType: typeof(PopUpConferma),
               defaultValue: false,
               defaultBindingMode: BindingMode.TwoWay,
               propertyChanged: IsPoppedPropertyChanged);

        public static readonly BindableProperty ConfermaColorProperty =
           BindableProperty.Create(
               propertyName: nameof(ConfermaColor),
               returnType: typeof(Color),
               declaringType: typeof(PopUpConferma),
               defaultValue: default(Color));


        public static readonly BindableProperty AnnullaColorProperty =
           BindableProperty.Create(
               propertyName: nameof(AnnullaColor),
               returnType: typeof(Color),
               declaringType: typeof(PopUpConferma),
               defaultValue: default(Color));


        public static readonly BindableProperty BorderColorProperty =
           BindableProperty.Create(
               propertyName: nameof(BorderColor),
               returnType: typeof(Color),
               declaringType: typeof(PopUpConferma),
               defaultValue: default(Color));

        public bool IsPopped { set => SetValue(IsPoppedProperty, value); get => (bool)GetValue(IsPoppedProperty); }
        public string PopUpText { set => SetValue(PopUpTextProperty, value); get => (string)GetValue(PopUpTextProperty); }

        public ICommand ConfermaCommand { set => SetValue(ConfermaCommandProperty, value); get => (ICommand)GetValue(ConfermaCommandProperty); }
        public ICommand AnnullaCommand { set => SetValue(AnnullaCommandProperty, value); get => (ICommand)GetValue(AnnullaCommandProperty); }

        public object ConfermaCommandParameter { set => SetValue(ConfermaCommandParameterProperty, value); get => GetValue(ConfermaCommandParameterProperty); }
        public object AnnullaCommandParameter { set => SetValue(AnnullaCommandParameterProperty, value); get => GetValue(AnnullaCommandParameterProperty); }

        public Color AnnullaColor { set => SetValue(AnnullaColorProperty, value); get => (Color)GetValue(AnnullaColorProperty); }
        public Color ConfermaColor { set => SetValue(ConfermaColorProperty, value); get => (Color)GetValue(ConfermaColorProperty); }
        public Color BorderColor { set => SetValue(BorderColorProperty, value); get => (Color)GetValue(BorderColorProperty); }

        public PopUpConferma()
        {
            InitializeComponent();
            BindingContext = this;
            IsVisible = IsPopped;
        }

        // Helper method for invoking commands safely
        public static void Execute(ICommand command, object param)
        {
            if (command == null) return;
            if (command.CanExecute(param))
            {
                command.Execute(param);
            }
        }


        public static void IsPoppedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (PopUpConferma)bindable;

            Debug.WriteLine("IsPopped Changed");

            control.IsPopped = (bool)newValue;
            control.IsVisible = (bool)newValue;


            if (control.IsPopped)
            {
                control.frame.ScaleTo(1, 200, Easing.CubicInOut);
            }
            else
            {
                control.frame.ScaleTo(0, 200, Easing.CubicInOut);
            }
        }

        public static void PopUpTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (PopUpConferma)bindable;
            control.PopUpText = newValue.ToString();
        }


        public void Conferma(object sender, EventArgs e)
        {
            Execute(ConfermaCommand, ConfermaCommandParameter);
            IsPopped = false;
        }

        public void Annulla(object sender, EventArgs e)
        {
            Execute(AnnullaCommand, AnnullaCommandParameter);
            IsPopped = false;
        }

    }
}
