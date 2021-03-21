using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        public Boolean EditMode = false;

        public void OnModifyProfileClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            
            if (EditMode == false)
            {
                button.Text = "Fatto";
                EditMode = true;
            }

            else if (EditMode == true)
            {
                button.Text = "Modifica Profilo";
                EditMode = false;
            }
        }
    }
}