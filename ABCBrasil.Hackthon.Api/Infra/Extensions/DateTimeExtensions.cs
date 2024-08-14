using System;

namespace ABCBrasil.Hackthon.Api.Infra.Extensions
{
    public static class DateTimeExtensions
    {
        public const string FORMAT_DATE_US = "yyyy-MM-dd";

        public static string ToOnlyDate(this DateTime date)
        {
            if (date == default)
            {
                return string.Empty;
            }

            return date.ToString(FORMAT_DATE_US);
        }

        public static string ToDateTimeRfc3339(this DateTime dateTime)
        {
            if (dateTime == default)
            {
                return string.Empty;
            }

            return dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}