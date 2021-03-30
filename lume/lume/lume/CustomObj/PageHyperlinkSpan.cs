using Xamarin.Forms;

namespace lume.CustomObj
{
    public class PageHyperlinkSpan : Span
    {
        public static readonly BindableProperty PageNameProperty =
            BindableProperty.Create(nameof(PageName), typeof(string), typeof(PageHyperlinkSpan), null);

        public static readonly BindableProperty NavigationProperty =
            BindableProperty.Create(nameof(Navigation), typeof(INavigation), typeof(PageHyperlinkSpan), null);

        public string PageName
        {
            set => SetValue(PageNameProperty, value);
            get => (string)GetValue(PageNameProperty);
        }

        public INavigation Navigation
        {
            set => SetValue(NavigationProperty, value);
            get => (INavigation)GetValue(NavigationProperty);
        }

        public PageHyperlinkSpan()
        {
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => await Navigation.PushModalAsync(PageFactory.IstanceNewPage(PageName))),
            });
        }
    }
}
