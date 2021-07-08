using System;
using System.Globalization;
using Xamarin.Forms;

namespace lume.CustomObj
{
    public class SelectedColorConverter : IValueConverter
    {
        public Color TrueColor { get; set; }
        public Color FalseColor { get; set; }
        public bool Invert { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (bool)value;
            if (Invert)
                v = !v;
            if (v)
                return TrueColor;
            else
                return FalseColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
