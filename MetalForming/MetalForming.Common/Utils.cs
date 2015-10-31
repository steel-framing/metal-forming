using System;
using System.Globalization;

namespace MetalForming.Common
{
    public static class Utils
    {
        public static bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
            return false;
#endif
            }
        }

        public static DateTime? ConvertDate(string dateValue, string format)
        {
            if (string.IsNullOrEmpty(dateValue)) return null;

            DateTime parsedDate;
            if (DateTime.TryParseExact(dateValue, format, null, DateTimeStyles.None, out parsedDate))
                return parsedDate;
            return null;
        }
    }
}
