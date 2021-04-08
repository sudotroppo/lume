
using lume.Templates;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationsPage : MainPageTemplate
    {
        public NotificationsPage()
        {
            InitializeComponent();
        }

		public async void OnProfileClicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfilePage());
		}
	}
}