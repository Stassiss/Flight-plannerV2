using System;

namespace API.Helper.Converter
{
    public static class Converter
    {
        public static string ConvertDateTimeToString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm").Replace("T", " ");
        }

        public static DateTime ConvertStringToDateTime(this string dateTimeString)
        {
            return DateTime.Parse(dateTimeString);
        }
    }
}
