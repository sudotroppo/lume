using lume.Assets;
using lume.Pages;
using lume.Templates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public static readonly BindableProperty TemplateContentProperty =
            BindableProperty.Create(nameof(TemplateContent), typeof(View), typeof(MainPage));

        public static readonly BindableProperty CurrentTabProperty =
           BindableProperty.Create(nameof(CurrentTab), typeof(ContentTemplatedView), typeof(MainPage));


        public ContentTemplatedView rootTab = new HomePage();

        private static readonly Color SelectedTabColor = (Color)Application.Current.Resources["SelectedTabColor"];

        private static readonly Color UnselectedTabColor = (Color)Application.Current.Resources["UnselectedTabsColor"];

        private readonly Dictionary<Type, View> TabDictionary;

        public Navigator navigator;

        private static readonly ArrayList Tab = new ArrayList { typeof(HomePage), typeof(FillRequestPage), typeof(NotificationsPage) };

        public new event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected new void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }


        private async void ChangeTab(object sender, EventArgs e)
        {
            Type currentTabType = CurrentTab.GetType();

            TabDictionary.TryGetValue(currentTabType, out View selectedTab);

            if (selectedTab != null)
            {
                (selectedTab as Button).TextColor = SelectedTabColor;

                foreach (View v in TabDictionary.Values)
                {
                    if (!v.Equals(selectedTab))
                    {
                        (v as Button).TextColor = UnselectedTabColor;
                    }
                }

                await Task.Run(() =>
                {
                    double columnWidth = Width / 3;
                    double swithTranslation = SelectedLine.TranslationX;
                    double initialScaleX = SelectedLine.ScaleX;

                    int index = Tab.IndexOf(currentTabType);

                    double dx = columnWidth * index - swithTranslation;

                    new Animation()
                    {
                        { 0, 1, Animations.SlideOfX(SelectedLine, dx, Easing.CubicInOut) },
                    }.Commit(this, "SwithTab", 1, 300, Easing.Linear);
                });


            }

        }

        public ContentTemplatedView CurrentTab
        {
            set
            {
                if (value != CurrentTab)
                {

                    SetValue(CurrentTabProperty, value);
                    TemplateContent = value.Content;
                    PropertyChanged += ChangeTab;
                    OnPropertyChanged("TemplateContent");
                }
            }


            get => (ContentTemplatedView)GetValue(CurrentTabProperty);
        }

        public View TemplateContent
        {
            set => SetValue(TemplateContentProperty, value);
            get => (View)GetValue(TemplateContentProperty);
        }

        public MainPage()
        {
            InitializeComponent();

            TabDictionary = new Dictionary<Type, View>
            {
                { typeof(HomePage), homeButton },
                { typeof(NotificationsPage), notificationButton },
                { typeof(FillRequestPage), fillrequestButton }
            };

            navigator = new Navigator(this);

            CurrentTab = rootTab;

            BindingContext = this;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public void OnNotificationClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            navigator.PushAsync(new NotificationsPage(navigator));
            (sender as Button).IsEnabled = true;
        }

        public void OnNewRequestClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            navigator.PushAsync(new FillRequestPage(navigator));
            (sender as Button).IsEnabled = true;

        }
        public void OnHomeClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            navigator.PushAsync(rootTab);
            (sender as Button).IsEnabled = true;
        }
    }
}