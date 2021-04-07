using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Plugin.SharedTransitions;
using lume.CustomObj;

namespace lume.Pages
{
    public class TabbedHomePage : LumeTabbedPage
    {
        public TabbedHomePage()
        {
            _ = On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            _ = On<Android>().SetIsSwipePagingEnabled(false);

            var home = new HomePage();
            var notif = new CustomNavigationPage(new NotificationsPage());

            home.IconImageSource = "home.png";
            notif.IconImageSource = "user.png";

            Children.Add(home);
            Children.Add(notif);

        }
        
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
