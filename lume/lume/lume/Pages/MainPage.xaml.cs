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
using System.Windows.Input;
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


        private string _popUpText = "Benvenuto!";
        public string PopUpText
        {
            set
            {
                _popUpText = value;
                OnPropertyChanged();
            }
            get
            {
                return _popUpText;
            }
        }

        private ICommand _popUpConferma;
        public ICommand PopUpConferma
        {
            set
            {
                _popUpConferma = value;
                OnPropertyChanged();
            }
            get
            {
                return _popUpConferma;
            }
        }

        private ICommand _popUpAnnulla;
        public ICommand PopUpAnnulla
        {
            set
            {
                _popUpAnnulla = value;
                OnPropertyChanged();
            }
            get
            {
                return _popUpAnnulla;
            }
        }

        private string _popUpConfermaNome;
        public string PopUpConfermaNome
        {
            set
            {
                _popUpConfermaNome = value;
                OnPropertyChanged();
            }
            get
            {
                return _popUpConfermaNome;
            }
        }

        private string _popUpAnnullaNome;
        public string PopUpAnnullaNome
        {
            set
            {
                _popUpAnnullaNome = value;
                OnPropertyChanged();
            }
            get
            {
                return _popUpAnnullaNome;
            }
        }

        private object _popUpConfermaParameter;
        public object PopUpConfermaParameter
        {
            set
            {
                _popUpConfermaParameter = value;
                OnPropertyChanged();
            }
            get
            {
                return _popUpConfermaParameter;
            }
        }

        private object _popUpAnnullaParameter;
        public object PopUpAnnullaParameter
        {
            set
            {
                _popUpAnnullaParameter = value;
                OnPropertyChanged();
            }
            get
            {
                return _popUpAnnullaParameter;
            }
        }

        private bool _isPopped;
        public bool IsPopped
        {
            set
            {
                _isPopped = value;
                OnPropertyChanged();
            }
            get
            {
                return _isPopped;
            }
        }

        public ContentTemplatedView rootTab = new HomePage();

        public void Alert(string msg,
            string confermaNome, ICommand confermaCommand, object confermaParameter,
            string annullaNome, ICommand annullaCommand, object annullaParameter)
        {
            PopUpText = msg;

            PopUpConferma = confermaCommand;
            PopUpConfermaNome = confermaNome;
            PopUpConfermaParameter = confermaParameter;

            PopUpAnnulla = annullaCommand;
            PopUpAnnullaNome = annullaNome;
            PopUpAnnullaParameter = annullaParameter;

            IsPopped = true;

        }


        public Navigator navigator { get; set; }


        public int CurrentTab { get; set; }

        private void ChangeTab(object sender, EventArgs e)
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


                Task.Run(() => SlideSelectorTo(CurrentTab));
            }
            catch(Exception) { }


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