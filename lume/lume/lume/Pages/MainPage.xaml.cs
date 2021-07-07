using lume.Assets;
using lume.Pages;
using lume.Templates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if ("TemplateContent".Equals(propertyName))
            {
                Debug.WriteLine($"propertyName = {propertyName}, \n\"emplateContent\".Equals(propertyName) = {"TemplateContent".Equals(propertyName)}");
                new PropertyChangedEventHandler(ChangeTab)?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                Debug.WriteLine($"propertyName = {propertyName}\n");

            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private static readonly Color SelectedTabColor = (Color)Application.Current.Resources["SelectedTabColor"];

        private static readonly Color UnselectedTabColor = (Color)Application.Current.Resources["UnselectedTabsColor"];


        public static readonly BindableProperty TemplateContentProperty =
            BindableProperty.Create(nameof(TemplateContent), typeof(View), typeof(MainPage));

        public static readonly BindableProperty CurrentTabProperty =
           BindableProperty.Create(nameof(CurrentTab), typeof(ContentTemplatedView), typeof(MainPage));



        public View TemplateContent
        {
            set
            {
                SetValue(TemplateContentProperty, value);
                //OnPropertyChanged();

            }
            get => (View)GetValue(TemplateContentProperty);
        }



        public ContentTemplatedView rootTab = new HomePage();

        private readonly Dictionary<Type, KeyValuePair<int, Button>> TabDictionary;



        public Navigator navigator;




        private async void ChangeTab(object sender, EventArgs e)
        {
            try
            {
                Type currentTabType = CurrentTab.GetType();

                KeyValuePair<int, Button> keyValuePair = TabDictionary[currentTabType];

                Button selectedTabButton = keyValuePair.Value;

                selectedTabButton.TextColor = SelectedTabColor;

                foreach (KeyValuePair<int, Button> v in TabDictionary.Values)
                {
                    if (!v.Value.Equals(selectedTabButton))
                    {
                        v.Value.TextColor = UnselectedTabColor;
                    }
                }

                await Task.Run(() => SlideSelectorTo(keyValuePair.Key));

                Debug.WriteLine($"tab to index: {keyValuePair.Key}");
            }
            catch { }


        }

        private void SlideSelectorTo(int index)
        {
            double columnWidth = Width / TabDictionary.Count;

            double swithTranslation = SelectedLine.TranslationX;

            double initialScaleX = SelectedLine.ScaleX;


            double dx = columnWidth * index - swithTranslation;

            new Animation()
            {
                { 0, 1, Animations.SlideOfX(SelectedLine, dx, Easing.CubicInOut) }

            }.Commit(this, "SwithTab", 1, 300, Easing.Linear);
        }

        public ContentTemplatedView CurrentTab
        {
            set
            {
                if (value != CurrentTab)
                {
                    SetValue(CurrentTabProperty, value);
                    TemplateContent = value.Content;
                    OnPropertyChanged();

                }
            }


            get => (ContentTemplatedView)GetValue(CurrentTabProperty);
        }


        public MainPage()
        {
            InitializeComponent();

            TabDictionary = new Dictionary<Type, KeyValuePair<int, Button>>
            {
                { typeof(HomePage), new KeyValuePair<int, Button>(0, HomePage_Button ) },
                { typeof(FillRequestPage), new KeyValuePair<int, Button>(1, FillRequestPage_Button) },
                { typeof(NotificationsPage), new KeyValuePair<int, Button>(2, NotificationsPage_Button) },
            };

            navigator = new Navigator(this);

            CurrentTab = rootTab;

            absolute.BindingContext = this;
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