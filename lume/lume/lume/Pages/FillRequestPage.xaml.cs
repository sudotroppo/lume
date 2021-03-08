using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FillRequestPage : ContentPage
    {
        public FillRequestPage()
        {
            InitializeComponent();

            var images = new List<string>
            {
                "https://www.hydromania.it/images/acquapark-roma/calcetto.jpg"
            };

            MainCarouselView.ItemsSource = images;
        }
    }
}