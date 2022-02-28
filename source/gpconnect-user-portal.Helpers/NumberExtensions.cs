using System;

namespace gpconnect_user_portal.Helpers
{
    public static class NumberExtensions
    {
        public static int StringToInteger(this string valueIn, int defaultValue)
        {
            return int.TryParse(valueIn, out _) ? Convert.ToInt32(valueIn) : defaultValue;
        }

        public static int StringToInteger(this string valueIn)
        {
            return valueIn == null ? 0 : StringToInteger(valueIn, 0);
        }

        public static string UnitsFormatter(this double valueIn, string units)
        {
            return $"{valueIn} {units}";
        }
    }

}
