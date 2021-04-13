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

        public async void ButtonClicked(Page nextPage)
        {
            var stack = Navigation.NavigationStack;
            Page currentPage = Parent as Page;
            Type currentPageType = currentPage.GetType();
            Type nextPageType = nextPage.GetType();

            if (currentPageType.Equals(nextPageType))
                return;

            Page resultPage = stack.ToList().Find(p => nextPageType.Equals(p.GetType()));

            if (resultPage is null)
            {
                resultPage = nextPage;
            }
            else
            {
                Navigation.RemovePage(resultPage);
            }
            
            await Navigation.PushAsync(resultPage, false);
        }

        public MainPageTemplate()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async void OnProfileClicked(object sender, EventArgs e)
        {
            ButtonClicked(new NotificationsPage());
        }

        public async void OnNewRequestClicked(object sender, EventArgs e)
        {
            ButtonClicked(new FillRequestPage());
        }
        public async void OnHomeClicked(object sender, EventArgs e)
        {
            ButtonClicked(new HomePage());
        }
    }
}