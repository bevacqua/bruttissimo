using System;
using System.Globalization;

namespace Bruttissimo.Common
{
    public static class DateTimeExtensions
    {
        public static string ToLongDateString(this DateTime date)
        {
			return date.ToString(Resources.User.LongDateFormat, CultureInfo.InvariantCulture); // TODO: culture based on user
        }

        public static string ToLongDateTimeString(this DateTime date)
        {
			return date.ToString(Resources.User.LongDateTimeFormat, CultureInfo.InvariantCulture); // TODO: culture based on user
        }

        public static string ToTimeAgoString(this DateTime since)
        {
            return since.ToTimeAgoString(DateTime.Now);
        }

        public static string ToTimeAgoString(this DateTime since, DateTime start)
        {
            var ts = new TimeSpan(start.Ticks - since.Ticks);
            return ts.ToTimeAgoString();
        }

        public static string ToDurationString(this DateTime since, DateTime start)
        {
            var ts = new TimeSpan(start.Ticks - since.Ticks);
            return ts.ToDurationString();
        }
    }
}