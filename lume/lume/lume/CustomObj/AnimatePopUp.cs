using System;
using System.Diagnostics;
using System.Globalization;
using lume.Domain;
using Xamarin.Forms;

namespace lume.CustomObj
{
    public class AnimatePopUp : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            View element = parameter as View;

            if ((bool) value && value != null)
            {
                Debug.WriteLine($"AnimatePopUp convert to True");
                element.TranslateTo(0, 0.35 * element.Height, 500, Easing.CubicInOut);
                return true;
            }
            else
            {
                Debug.WriteLine($"AnimatePopUp convert to False, Heigth = {element.Height}");
                element.TranslateTo(0, element.Height, 500, Easing.CubicInOut);
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
