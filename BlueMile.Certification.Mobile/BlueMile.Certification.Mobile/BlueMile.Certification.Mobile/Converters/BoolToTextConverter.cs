using System;
using System.Globalization;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.Converters
{
    public class BoolToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Yes" : "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "Yes" ? true : false;
        }
    }
}
