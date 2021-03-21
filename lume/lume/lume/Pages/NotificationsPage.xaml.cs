
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using lume.CustomObj;

namespace lume.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationsPage : ContentPage
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