using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Extender
{
    public static class ConvertExtesions
    {
        public static TR ChangeType<TR>(this object value)
        {
            return (TR)ChangeType(value, typeof(TR));
        }


        public static TR ChangeType<TR>(this object value, TR whenNull)
        {
            return (value == null || value is DBNull)
                ? whenNull
                : (TR)ChangeType(value, typeof(TR));
        }

        public static object ChangeType(this object value, Type convertToType)
        {
            if (convertToType == null)
            {
                throw new ArgumentNullException("convertToType");
            }

            if (value == null || value is DBNull)
            {
                return null;
            }

            if (convertToType.IsGenericType &&
                convertToType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                convertToType = Nullable.GetUnderlyingType(convertToType);
            }

            if (convertToType.IsEnum && value is string)
            {
                return Enum.Parse(convertToType, value as string);
            }

            if (value != null && convertToType.IsEnum && value.GetType().IsPrimitive &&
                !(value is bool) && !(value is char) &&
                !(value is float) && !(value is double))
            {
                return Enum.ToObject(convertToType, value);
            }

            return Convert.ChangeType(value, convertToType, CultureInfo.InvariantCulture);
        }
    }
}
