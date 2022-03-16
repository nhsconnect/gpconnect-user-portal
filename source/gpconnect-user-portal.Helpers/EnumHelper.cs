using System;
using System.ComponentModel;

namespace gpconnect_user_portal.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var value = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    value = ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return value;
        }

        public static T ToEnum<T>(this string enumString) where T : struct
        {
            T resultInputType = default(T);
            if (!typeof(T).IsEnum) return default(T);
            var isEnumParsed = Enum.TryParse(enumString, true, out resultInputType);
            return resultInputType;
        }
    }
}
