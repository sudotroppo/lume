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
                Debug.WriteLine($"propertyName = {propertyName}, \n\"TemplateContent\".Equals(propertyName) = {"TemplateContent".Equals(propertyName)}");
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

        public List<ContentTemplatedView> Tabs;

        public List<Button> ButtonTabs;

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



        public Navigator navigator;


        public int CurrentTab;

        private async void ChangeTab(object sender, EventArgs e)
        {
            try
            {

                for(int i = 0; i < Tabs.Capacity; i++)
                {
                    if(i != CurrentTab)
                    {
                        ButtonTabs[i].TextColor = UnselectedTabColor;

                    }
                    else
                    {
                        ButtonTabs[i].TextColor = SelectedTabColor;
                    }
                }


                await Task.Run(() => SlideSelectorTo(CurrentTab));
            }
            catch { }


        }

        private void SlideSelectorTo(int index)
        {
            double columnWidth = Width / ButtonTabs.Count;

            double swithTranslation = SelectedLine.TranslationX;

            double initialScaleX = SelectedLine.ScaleX;


            double dx = columnWidth * index - swithTranslation;

            new Animation()
            {
                { 0, 1, Animations.SlideOfX(SelectedLine, dx, Easing.CubicInOut) }

            }.Commit(this, "SwithTab", 1, 300, Easing.Linear);
        }


        public void TabChanged()
        {
            TemplateContent = Tabs[CurrentTab].Content;
            OnPropertyChanged();

        }

        public MainPage()
        {
            InitializeComponent();

            navigator = new Navigator(this);

            Tabs = new List<ContentTemplatedView>(3)
            {
                new HomePage(),
                new FillRequestPage(navigator),
                new NotificationsPage(navigator)
            };

            ButtonTabs = new List<Button>(3)
            {
                HomePage_Button,
                FillRequestPage_Button,
                NotificationsPage_Button
            };

            navigator.GoTo(0);

            absolute.BindingContext = this;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public void OnNotificationClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            navigator.GoTo(2);
            (sender as Button).IsEnabled = true;
        }

        public void OnNewRequestClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            navigator.GoTo(1);
            (sender as Button).IsEnabled = true;

        }

        public void OnHomeClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            navigator.GoTo(0);
            (sender as Button).IsEnabled = true;
        }
    }
}