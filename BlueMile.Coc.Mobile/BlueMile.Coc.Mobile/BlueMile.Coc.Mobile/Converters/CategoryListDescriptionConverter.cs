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
    public class CategoryListDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return String.Empty;
            }

            return GetDescription((List<CategoryStaticEntity>)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public static List<string> GetDescription(List<CategoryStaticEntity> enumList)
        {
            var displayList = new List<string>();
            foreach (var item in enumList)
            {
                Type type = item.GetType();
                MemberInfo[] memInfo = type.GetMember(item.ToString());
                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attrs != null && attrs.Length > 0)
                    {
                        displayList.Add(((DescriptionAttribute)attrs[0]).Description);
                    }
                    else
                    {
                        displayList.Add(item.ToString());
                    }
                }
                else
                {
                    displayList.Add(item.ToString());
                }
            }
            return displayList;
        }
    }
}
