namespace Occurify.TimeZones.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="TimeOnly"/>.
    /// </summary>
    public static class TimeOnlyExtensions
    {
        /// <summary>
        /// Converts a <paramref name="timeOnly"/> to a cron expression.
        /// </summary>
        public static string ToCronExpression(this TimeOnly timeOnly)
        {
            if (timeOnly.Millisecond != 0)
            {
                throw new ArgumentException("Milliseconds are not supported in cron expressions.");
            }
#if NET7_0 || NET8_0 || NET9_0
            if (timeOnly.Microsecond != 0)
            {
                throw new ArgumentException("Microseconds are not supported in cron expressions.");
            }
            if (timeOnly.Nanosecond != 0)
            {
                throw new ArgumentException("Nanoseconds are not supported in cron expressions.");
            }
#endif

            if (timeOnly.Second != 0)
            {
                return $"{timeOnly.Second} {timeOnly.Minute} {timeOnly.Hour} * * *";
            }
            return $"{timeOnly.Minute} {timeOnly.Hour} * * *";
        }
    }
}
