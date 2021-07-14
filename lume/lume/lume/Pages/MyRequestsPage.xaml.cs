using System;
using System.Collections.Generic;
using lume.Templates;
using lume.ViewModels;
using lume.Assets;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace lume.Pages
{
    public partial class MyRequestsPage : ContentTemplatedView
    {
        public MyRequestsPage()
        {
            InitializeComponent();
        }

        public async void OnBackButtonCliked(object sender, EventArgs e)
        {
            Button b = (sender as Button);
            b.IsEnabled = false;

            Animation ToSettingsPage = new Animation  // animazione di cambio pagina
            {
                {0, 1, Animations.SlideOfX(ToTheLeft, -50, Easing.CubicInOut) },
                {0, 1, Animations.FadeTo(TitleLayout, 0, Easing.CubicInOut) },
                {0, 1, Animations.FadeTo(mainLayout, 0, Easing.CubicInOut) }
            };

            await Task.Run(() => ToSettingsPage.Commit(this, "ToSettingsPage", 1, 500, Easing.Linear, (c, v) =>
            {
                navigator.InsetPageIntoTabIndex(new SettingsPage(navigator), 2);
                b.IsEnabled = true;
            }));
        }
    }
}
