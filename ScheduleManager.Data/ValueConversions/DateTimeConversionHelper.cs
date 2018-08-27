using System;

namespace ScheduleManager.Data.ValueConversions
{
    public class DateTimeConversionHelper
    {
        public static long ToFileTimeUtc(DateTime dateTime)
        {
            return dateTime.ToFileTimeUtc();
        }

        public static DateTime FromFileTimeUtc(long time)
        {
            return DateTime.FromFileTimeUtc(time);
        }

        public static long? ToFileTimeUtc(DateTime? dateTime)
        {
            return dateTime?.ToFileTimeUtc();
        }

        public static DateTime? FromFileTimeUtc(long? time)
        {
            return time == null ? new DateTime?() : DateTime.FromFileTimeUtc(time.Value);
        }
    }
}