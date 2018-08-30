using System;

namespace ScheduleManager.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool iEquals(this string source, string second)
        {
            if (source == null && second == null)
                return true;

            if (source == null)
                return false;

            return source.Equals(second, StringComparison.OrdinalIgnoreCase);
        }
    }
}