using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class EnumExtensions
    {
        public static string EnumDescription(this Enum value)
        {
            var info = value.GetType().GetField(value.ToString());

            return info.GetCustomAttributes<EnumDescriptionAttribute>(false)
                            .Select(x => x.Value)
                            .FirstOrDefault();

        }

        public static T GetEnum<T>(this string value)
        {
            var fieldInfo = typeof(T).GetFields()
                .FirstOrDefault(x => x.GetCustomAttributes(typeof(EnumDescriptionAttribute), false).Cast<EnumDescriptionAttribute>()
                .Any(z1 => z1.Value == value));

            if (fieldInfo != null)
            {
                var enumValue = fieldInfo.GetValue(null);
                return (T)enumValue;
            }

            return default(T);
        }

        public static T? GetEnumOrNull<T>(this string value) where T : struct
        {
            var fieldInfo = typeof(T).GetFields()
                .FirstOrDefault(x => x.GetCustomAttributes(typeof(EnumDescriptionAttribute), false).Cast<EnumDescriptionAttribute>()
                .Any(z1 => z1.Value == value));

            if (fieldInfo != null)
            {
                var enumValue = fieldInfo.GetValue(null);
                return (T)enumValue;
            }

            return null;
        }

        public static T AddFlag<T>(this Enum type, T value)
        {
            return (T)(object)(((int)(object)type | (int)(object)value));
        }

        public static T RemoveFlag<T>(this Enum type, T value)
        {
            return (T)(object)(((int)(object)type & ~(int)(object)value));
        }
    }

    public class EnumDescriptionAttribute : Attribute
    {
        public string Value { get; set; }

        public EnumDescriptionAttribute(string value)
        {
            Value = value;
        }
    }

}
