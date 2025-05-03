
namespace Occurify.TimeZones.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts <paramref name="utcDateTime"/> from Coordinated Universal Time (UTC) to <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static DateTime UtcToLocalTimeZone(this DateTime utcDateTime)
            => utcDateTime.UtcToTimeZone(TimeZoneInfo.Local);

        /// <summary>
        /// Converts <paramref name="utcDateTime"/> from Coordinated Universal Time (UTC) to <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static DateTime? UtcToLocalTimeZone(this DateTime? utcDateTime)
            => utcDateTime.UtcToTimeZone(TimeZoneInfo.Local);

        /// <summary>
        /// Converts <paramref name="utcDateTime"/> from Coordinated Universal Time (UTC) to timezone <paramref name="timeZone"/>.
        /// </summary>
        public static DateTime UtcToTimeZone(this DateTime utcDateTime, TimeZoneInfo timeZone)
        {
            if (utcDateTime.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
            }

            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZone);
        }

        /// <summary>
        /// Converts <paramref name="utcDateTime"/> from Coordinated Universal Time (UTC) to timezone <paramref name="timeZone"/>.
        /// </summary>
        public static DateTime? UtcToTimeZone(this DateTime? utcDateTime, TimeZoneInfo timeZone) => 
            utcDateTime?.TimeZoneToUtc(timeZone);

        /// <summary>
        /// Converts <paramref name="localDateTime"/> from <see cref="TimeZoneInfo.Local"/> to Coordinated Universal Time (UTC).
        /// </summary>
        public static DateTime LocalTimeZoneToUtc(this DateTime localDateTime)
            => localDateTime.TimeZoneToUtc(TimeZoneInfo.Local);

        /// <summary>
        /// Converts <paramref name="localDateTime"/> from <see cref="TimeZoneInfo.Local"/> to Coordinated Universal Time (UTC).
        /// </summary>
        public static DateTime? LocalTimeZoneToUtc(this DateTime? localDateTime)
            => localDateTime.TimeZoneToUtc(TimeZoneInfo.Local);

        /// <summary>
        /// Converts <paramref name="dateTime"/> from timezone <paramref name="sourceTimeZone"/> to Coordinated Universal Time (UTC).
        /// </summary>
        public static DateTime TimeZoneToUtc(this DateTime dateTime, TimeZoneInfo sourceTimeZone) =>
            TimeZoneInfo.ConvertTimeToUtc(dateTime, sourceTimeZone);

        /// <summary>
        /// Converts <paramref name="dateTime"/> from timezone <paramref name="sourceTimeZone"/> to Coordinated Universal Time (UTC).
        /// </summary>
        public static DateTime? TimeZoneToUtc(this DateTime? dateTime, TimeZoneInfo sourceTimeZone) =>
            dateTime?.TimeZoneToUtc(sourceTimeZone);
    }
}
