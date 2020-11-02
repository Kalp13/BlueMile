using BlueMile.Coc.Data;
using BlueMile.Coc.Mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace BlueMile.Coc.Mobile.Converters
{
    public class CategoryDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return String.Empty;
            }

            return GetDescription((CategoryStaticEntity)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public static string GetDescription(CategoryStaticEntity x)
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
