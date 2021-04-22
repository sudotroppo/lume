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
            BindableProperty.Create(nameof(TemplateContent), typeof(View), typeof(MainPageTemplate));

        public static readonly BindableProperty CurrentTabProperty =
           BindableProperty.Create(nameof(CurrentTab), typeof(ContentTemplatedView), typeof(MainPageTemplate));

        public Navigator navigator;

        public ContentTemplatedView rootTab = new HomePage();

        public Color SelectedTabColor = (Color)Application.Current.Resources["SelectedTabColor"];

        public Color UnselectedTabColor = (Color)Application.Current.Resources["UnselectedTabsColor"];

        public ContentTemplatedView CurrentTab 
        {
            set => OnTabSetted(value);
            get => (ContentTemplatedView)GetValue(CurrentTabProperty);
        }

        private void OnTabSetted(ContentTemplatedView value)
        {
            SetValue(CurrentTabProperty, value);

            deselectAllButtons();
            if (value.GetType().Equals(typeof(HomePage)))
            {

                homeButton.TextColor = SelectedTabColor;
            }
            else
            {
                notificationButton.TextColor = SelectedTabColor;
            }
            TemplateContent = value.Content;
        }

        public View TemplateContent
        {
            set => SetValue(TemplateContentProperty, value);
            get => (View)GetValue(TemplateContentProperty);
        }

        public MainPageTemplate()
        {
            InitializeComponent();
            navigator = new Navigator(this);
            CurrentTab = rootTab;
            BindingContext = this;
        }

        private void deselectAllButtons()
        {
            notificationButton.TextColor = UnselectedTabColor;
            homeButton.TextColor = UnselectedTabColor;
        }

        public async void OnProfileClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            await navigator.PushAsync(new NotificationsPage(navigator));
            (sender as Button).IsEnabled = true;
        }

        public void OnNewRequestClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;

            (sender as Button).IsEnabled = true;
        }
        public async void OnHomeClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            if (!CurrentTab.GetType().Equals(typeof(HomePage)))
                await navigator.PushAsync(rootTab);
            (sender as Button).IsEnabled = true;
        }
    }
}