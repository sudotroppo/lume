using Xamarin.Forms;

namespace lume.Templates
{
    public partial class PostTemplate : ContentView
    {
        private UriImageSource ProfileImage { set; get; }

        private string UserFullName { set; get; }

        private int Number { set; get; }

        private string PostDescription { set; get; }

        public PostTemplate()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
