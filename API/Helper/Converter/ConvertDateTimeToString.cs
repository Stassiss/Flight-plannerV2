using System;

namespace API.Helper.Converter
{
    public static class ConvertDateTimeToString
    {
        public static string CovertToString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm").Replace("T", " ");
        }
    }
}
