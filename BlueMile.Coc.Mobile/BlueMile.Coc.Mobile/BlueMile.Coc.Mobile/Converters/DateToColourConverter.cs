using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BlueMile.Coc.Mobile.Converters
{
    public class DateToColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var underSixM = DateTime.Compare(DateTime.Today.AddMonths(6), (DateTime)value);
            var betweenSixAndTwelve = DateTime.Compare(DateTime.Today.AddMonths(12), (DateTime)value);
            if (underSixM >= 0)
            {
                return Color.FromHex("FF4D4D");
            }
            else if (underSixM < 0 && betweenSixAndTwelve >= 0)
            {
                return Color.FromHex("FFBB4D");
            }
            else
            {
                return Color.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
