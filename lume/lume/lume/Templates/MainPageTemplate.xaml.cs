using lume.Pages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageTemplate : ContentView
    {
        public static readonly BindableProperty TemplateContentProperty =
            BindableProperty.Create(nameof(TemplateContent), typeof(View), typeof(MainPageTemplate));

        public View TemplateContent
        {
            set => SetValue(TemplateContentProperty, value);
            get => (View)GetValue(TemplateContentProperty);
        }

        public MainPageTemplate()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async void OnProfileClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            await Navigator.PushAsync(Navigation, new NotificationsPage(), Parent as Page, false);
            (sender as Button).IsEnabled = true;
        }

        public async void OnNewRequestClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            await Navigator.PushAsync(Navigation, new FillRequestPage(), Parent as Page, false);
            (sender as Button).IsEnabled = true;
        }
        public async void OnHomeClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            await Navigator.PushAsync(Navigation, new HomePage(), Parent as Page, false);
            (sender as Button).IsEnabled = true;
        }
    }
}