
using lume.Templates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentTemplatedView
    {
        public SettingsPage(MainPageTemplate Control) : base(Control)
        {
            InitializeComponent();
        }
    }
}