using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TPage : Xamarin.Forms.TabbedPage
    {
        public TPage()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }

        int count = 1;

        async void AreUSoure(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Chiudi", "Seisicuro di voler uscire?", "no", "si");
            if (answer)
            {
                await DisplayAlert("no","","ok");
            }
            else
            {
                await DisplayAlert("si", "", "ok");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (count == 0x2)
            {
                AreUSoure(this, null);
                count = 0;
            }
            count++;
            
            return true;
        }
    }
}
