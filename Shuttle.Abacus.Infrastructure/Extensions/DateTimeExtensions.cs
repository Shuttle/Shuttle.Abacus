using System;

namespace Shuttle.Abacus.Infrastructure
{
    public static class DateTimeExtensions
    {
        public static DateTime MoveTime(this DateTime fromDate, DateTime toDate)
        {
            return DateTime.Parse(toDate.ToString("dd MMM yyyy ") + fromDate.ToString("HH:mm:ss"));
        }

        public static DateTime StripSeconds(this DateTime dateTime)
        {
            return dateTime.Subtract(new TimeSpan(0, 0, dateTime.Second));
        }
    }
}
