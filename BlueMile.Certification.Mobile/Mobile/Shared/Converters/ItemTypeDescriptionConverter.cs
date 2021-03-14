using BlueMile.Certification.Mobile.Data.Static;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.Converters
{
    public class ItemTypeDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return String.Empty;
            }

            return GetDescription((ItemTypeEnum)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public static string GetDescription(ItemTypeEnum x)
        {
            Type type = x.GetType();
            MemberInfo[] memInfo = type.GetMember(x.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return x.ToString();
        }
    }
}
