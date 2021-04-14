using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace lume
{
    //classe per la navigazione tra pagine all'interno di una NavigationPage
    public static class Navigator
    {
        public static Task PushAsync(INavigation Navigation, Page nextPage, Page currentPage, bool animated)
        {
            var stack = Navigation.NavigationStack;
            Type currentPageType = currentPage.GetType();
            Type nextPageType = nextPage.GetType();

            if (currentPageType.Equals(nextPageType))
                return Task.CompletedTask;

            Page resultPage = stack.ToList().Find(p => nextPageType.Equals(p.GetType()));

            if (resultPage is null)
            {
                resultPage = nextPage;
            }
            else
            {
                Navigation.RemovePage(resultPage);
            }

            return Navigation.PushAsync(resultPage, false);
        }
    }
}
