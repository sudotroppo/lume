using System;
using lume.Templates;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace lume.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class SettingsPage : ContentTemplatedView    {        public SettingsPage(Navigator navigator) : base(navigator)        {            InitializeComponent();        }

        void ToTheLeft_Clicked(object sender, EventArgs e)
        {

        }
    }}