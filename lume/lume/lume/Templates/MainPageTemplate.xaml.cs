using lume.Pages;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageTemplate : ContentPage
    {
        public static readonly BindableProperty TemplateContentProperty =
            BindableProperty.Create(nameof(TemplateContent), typeof(View), typeof(MainPageTemplate), new HomePage().Content);

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

        public void OnProfileClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            SetValue(TemplateContentProperty, new NotificationsPage(this).Content);
            (sender as Button).IsEnabled = true;
        }

        public void OnNewRequestClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            SetValue(TemplateContentProperty, new FillRequestPage(this).Content);
            (sender as Button).IsEnabled = true;
        }
        public void OnHomeClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            SetValue(TemplateContentProperty, new HomePage(this).Content);
            (sender as Button).IsEnabled = true;
        }
    }
}