using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace quicky.bakkers.Converters
{
    public class BoolToFontAttributeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return FontAttributes.None;

            bool result;
            if(bool.TryParse(value.ToString(), out result))
                return (result ? FontAttributes.Bold : FontAttributes.None);

            return FontAttributes.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
