using System;
using System.Diagnostics;
using System.Globalization;
using lume.Domain;
using Xamarin.Forms;

namespace lume.CustomObj
{
    public class ButtonSwitch : IValueConverter
    {
        public Color OffColor { set; get; }
        public Color OnColor { set; get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null) { return false; }
            Button button = parameter as Button;

            if(value != null)
            {
                button.BackgroundColor = OnColor;
                return true;
            }
            else
            {
                button.BackgroundColor = OffColor;
                return false;
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
