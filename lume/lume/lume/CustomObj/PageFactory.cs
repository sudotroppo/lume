using System;
using Xamarin.Forms;

namespace lume.CustomObj
{
    public class PageFactory
    {
        private static string prefix = "lume.Pages.";

        public static ContentPage IstanceNewPage(string pageClassName, ContentPage defaultPage)
        {
            string fullyQualifiedName = prefix + pageClassName;
            Type pageType = Type.GetType(fullyQualifiedName);


            ContentPage page = (ContentPage)Activator.CreateInstance(pageType);

            return page ?? defaultPage;
        }

        public static ContentPage IstanceNewPage(string pageClassName)
        {
            return IstanceNewPage(pageClassName, new ContentPage());
        }

    }
}
