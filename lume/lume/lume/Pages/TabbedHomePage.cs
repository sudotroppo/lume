﻿using System;
using Xamarin.Forms;
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
                Navigation.RemovePage(page);
            }
            _ = On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            _ = On<Android>().SetIsSwipePagingEnabled(false);

            var home = new NavigationPage(new HomePage());
            var notif = new NavigationPage(new NotificationsPage());
            var fillreq = new NavigationPage(new FillRequestPage());

            home.IconImageSource = "home.png";
            fillreq.IconImageSource = "plus.png";
            notif.IconImageSource = "user.png";

            Children.Add(home);
            Children.Add(fillreq);
            Children.Add(notif);

        }
    }
}
