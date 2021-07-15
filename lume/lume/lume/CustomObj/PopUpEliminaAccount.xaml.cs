using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using lume.Pages;
using lume.Utility;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.CustomObj
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpEliminaAccount : ContentView
    {


        public static readonly BindableProperty PopUpTextProperty =
           BindableProperty.Create(
               propertyName: nameof(PopUpText),
               returnType: typeof(string),
               declaringType: typeof(PopUpEliminaAccount),
               defaultValue: "",
               defaultBindingMode: BindingMode.TwoWay,
               propertyChanged: PopUpTextPropertyChanged);

        public static readonly BindableProperty IsPoppedProperty =
           BindableProperty.Create(
               propertyName: nameof(IsPopped),
               returnType: typeof(bool),
               declaringType: typeof(PopUpEliminaAccount),
               defaultValue: false,
               defaultBindingMode: BindingMode.TwoWay,
               propertyChanged: IsPoppedPropertyChanged);

        public static readonly BindableProperty ConfermaColorProperty =
           BindableProperty.Create(
               propertyName: nameof(ConfermaColor),
               returnType: typeof(Color),
               declaringType: typeof(PopUpEliminaAccount),
               defaultValue: default(Color));


        public static readonly BindableProperty AnnullaColorProperty =
           BindableProperty.Create(
               propertyName: nameof(AnnullaColor),
               returnType: typeof(Color),
               declaringType: typeof(PopUpEliminaAccount),
               defaultValue: default(Color));


        public static readonly BindableProperty BorderColorProperty =
           BindableProperty.Create(
               propertyName: nameof(BorderColor),
               returnType: typeof(Color),
               declaringType: typeof(PopUpEliminaAccount),
               defaultValue: default(Color));


        public static readonly BindableProperty ConfermaTextProperty =
            BindableProperty.Create(
                propertyName: nameof(ConfermaText),
                returnType: typeof(string),
                declaringType: typeof(PopUpEliminaAccount),
                defaultValue: "Conferma",
                defaultBindingMode: BindingMode.TwoWay);


        public static readonly BindableProperty AnnullaTextProperty =
            BindableProperty.Create(
                propertyName: nameof(AnnullaText),
                returnType: typeof(string),
                declaringType: typeof(PopUpEliminaAccount),
                defaultValue: "Annulla",
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty PasswordProperty =
            BindableProperty.Create(
                propertyName: nameof(Password),
                returnType: typeof(string),
                declaringType: typeof(PopUpEliminaAccount),
                defaultValue: null,
                defaultBindingMode: BindingMode.TwoWay);

        public bool IsPopped { set => SetValue(IsPoppedProperty, value); get => (bool)GetValue(IsPoppedProperty); }

        public string PopUpText { set => SetValue(PopUpTextProperty, value); get => (string)GetValue(PopUpTextProperty); }
        public string Password { get => (string)GetValue(PasswordProperty); set => SetValue(PasswordProperty, value); }
        
        public string ConfermaText { get => (string)GetValue(ConfermaTextProperty); set => SetValue(ConfermaTextProperty, value); }
        public string AnnullaText { get => (string)GetValue(AnnullaTextProperty); set => SetValue(AnnullaTextProperty, value); }

        public Color AnnullaColor { set => SetValue(AnnullaColorProperty, value); get => (Color)GetValue(AnnullaColorProperty); }
        public Color ConfermaColor { set => SetValue(ConfermaColorProperty, value); get => (Color)GetValue(ConfermaColorProperty); }
        public Color BorderColor { set => SetValue(BorderColorProperty, value); get => (Color)GetValue(BorderColorProperty); }

        public bool IsConfermaVisibile { get => !"".Equals(ConfermaText); }
        public bool IsAnnullaVisibile { get => !"".Equals(AnnullaText); }

        public bool _isLoading = false;
        public bool IsLoading { get => _isLoading; set{ _isLoading = value; OnPropertyChanged(); } }


        public PopUpEliminaAccount()
        {
            InitializeComponent();
            IsVisible = IsPopped;
        }


        public static void IsPoppedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (PopUpEliminaAccount)bindable;

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
            var control = (PopUpPassword)bindable;
            control.PopUpText = (string)newValue;
        }

        public bool CheckUserPassword()
        {
            try
            {
                return DataAccess.CheckPassword(Password);

            }
            catch (Exception)
            {
                nav.Alert("si è verificato un probleme, riprovare più tardi", "", "ok");
                return false;
            }
        }

        private Navigator nav = ((Application.Current.MainPage as CustomNavigationPage).CurrentPage as MainPage).navigator;

        public void Conferma(object sender, EventArgs e)
        {
            IsLoading = true;

            Task.Run(() =>
            {

                if (Password == null)
                {
                    nav.Alert("Immetti la password", "", "ok");
                }

                if (!"".Equals(Password) && CheckUserPassword())
                {
                    DataAccess.DeleteUtente();
                    Password = null;


                    Device.BeginInvokeOnMainThread( async () =>
                    {
                        IsLoading = false;
                        IsPopped = false;

                        await SecureStorage.SetAsync("token", "");
                        await SecureStorage.SetAsync("email", "");
                        await Application.Current.MainPage.DisplayAlert("Il tuo account è stato eliminato con successo", "", "ok");

                        await (Application.Current.MainPage as CustomNavigationPage).Navigation.PopAsync();

                    });


                }
                else
                {
                    Device.BeginInvokeOnMainThread(() => IsLoading = false);
                    nav.Alert("Password errata", "", "ok");

                }
            });
        }

        public void Annulla(object sender, EventArgs e)
        {
            IsPopped = false;
        }

    }
}
