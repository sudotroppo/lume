using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace lume.Pages
{
    public class TabbedHomePage : LumeTabbedPage
    {
        public TabbedHomePage()
        {

            var existingPages = Navigation.NavigationStack;
            foreach (var page in existingPages)
            {
                if (page is LoginPage)
                    Navigation.RemovePage(page);
            }
            _ = On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            _ = On<Android>().SetIsSwipePagingEnabled(false);

            var home = new HomePage();
            var notif = new NavigationPage(new NotificationsPage());
            var fillreq = new FillRequestPage();

            home.IconImageSource = "home.png";
            fillreq.IconImageSource = "plus.png";
            notif.IconImageSource = "user.png";

            Children.Add(home);
            Children.Add(fillreq);
            Children.Add(notif);

        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
