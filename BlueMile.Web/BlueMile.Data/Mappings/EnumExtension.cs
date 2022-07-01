using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BlueMile.Data.Mappings
{
    public static class EnumExtensions
    {
        public static string ToDisplayName(this Enum value)
        {
            Type type = value.GetType();

            MemberInfo[] memInfo = type.GetMember(value.ToString());

            // no attributes found ?
            if (memInfo.Length == 0) return string.Empty;

            // cast to Array or List required 
            // an IEnumerable of DisplayAttribute cannot be indexed !
            DisplayAttribute[] attributes = memInfo[0].GetCustomAttributes<DisplayAttribute>(false).ToArray();

            // handle value without description
            return (attributes.Length == 0) ? string.Empty : attributes[0].Name;
        }

        public static int ToOrder(this Enum value)
        {
            Type type = value.GetType();

            MemberInfo[] memInfo = type.GetMember(value.ToString());

            // no attributes found ?
            if (memInfo.Length == 0) return 0;

            // cast to Array or List required 
            // an IEnumerable of DisplayAttribute cannot be indexed !
            DisplayAttribute[] attributes = memInfo[0].GetCustomAttributes<DisplayAttribute>(false).ToArray();

            // handle value without description
            return (attributes.Length == 0) ? 0 : attributes[0].Order;
        }

        public static bool IsActive(this Enum value)
        {
            Type type = value.GetType();

            MemberInfo[] memInfo = type.GetMember(value.ToString());

            // no attributes found ?
            if (memInfo.Length == 0) return true;

            // cast to Array or List required 
            // an IEnumerable of ObsoleteAttribute cannot be indexed !
            ObsoleteAttribute[] attributes = memInfo[0].GetCustomAttributes<ObsoleteAttribute>(false).ToArray();

            // handle value without description
            return (attributes.Length == 0);
        }
    }
}
