
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using lume;
using lume.Templates;

namespace lume.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{

		private bool EditMode = false;
		private IList<View> InfoList;

		public ProfilePage()
		{
		   InitializeComponent();
		   InfoList = InfoStack.Children;
		}


		private void SwitchButtonState(string status, Button button)
		{

			var verdeLume = (Color)Application.Current.Resources["VerdeLume"];
			var nero = (Color)Application.Current.Resources["Nero"];
			var grigioChiaro = (Color)Application.Current.Resources["GrigioLume"];
			var bianco = (Color)Application.Current.Resources["BiancoLume"];

			// Quando l'utente si trova in stato di modifica
			if (status == "edit")
			{
				button.Text = "Fatto";
				button.TextColor = bianco;
				button.BackgroundColor = verdeLume;
			}

			// Stato di default
			else
			{
				button.Text = "Modifica profilo";
				button.TextColor = nero;
				button.BackgroundColor = grigioChiaro;
			}

		}

		public void OnModifyProfileClicked(object sender, EventArgs e)
		{
			Button button = (Button)sender;

			EditMode = !EditMode;

			if (EditMode)
				SwitchButtonState("edit", button);
			else
				SwitchButtonState("default", button);

			foreach (var child in InfoList)
			{
				if (child is InfoView info)
				{
					info.IsEditable = EditMode;
				}
			}

		}
	}
}