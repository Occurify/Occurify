
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
        public static DateTime? ToLocalTime(this DateTime? utcDateTime)
            => utcDateTime.ToTimeZone(TimeZoneInfo.Local);

        /// <summary>
        /// Converts <paramref name="utcDateTime"/> from Coordinated Universal Time (UTC) to timezone <paramref name="timeZone"/>.
        /// </summary>
        public static DateTime ToTimeZone(this DateTime utcDateTime, TimeZoneInfo timeZone)
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
        public static DateTime? ToTimeZone(this DateTime? utcDateTime, TimeZoneInfo timeZone) => 
            utcDateTime?.ToUniversalTime(timeZone);

        /// <summary>
        /// Converts <paramref name="localDateTime"/> from <see cref="TimeZoneInfo.Local"/> to Coordinated Universal Time (UTC).
        /// </summary>
        public static DateTime? ToUniversalTime(this DateTime? localDateTime)
            => localDateTime.ToUniversalTime(TimeZoneInfo.Local);

        /// <summary>
        /// Converts <paramref name="dateTime"/> from timezone <paramref name="sourceTimeZone"/> to Coordinated Universal Time (UTC).
        /// </summary>
        public static DateTime ToUniversalTime(this DateTime dateTime, TimeZoneInfo sourceTimeZone) =>
            TimeZoneInfo.ConvertTimeToUtc(dateTime, sourceTimeZone);

        /// <summary>
        /// Converts <paramref name="dateTime"/> from timezone <paramref name="sourceTimeZone"/> to Coordinated Universal Time (UTC).
        /// </summary>
        public static DateTime? ToUniversalTime(this DateTime? dateTime, TimeZoneInfo sourceTimeZone) =>
            dateTime?.ToUniversalTime(sourceTimeZone);

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent string representation using the formatting conventions of the current culture in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToString(this DateTime dateTime, TimeZoneInfo timeZone) => dateTime.ToTimeZone(timeZone).ToString(null, null);

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent string representation using the specified culture-specific format information in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToString(this DateTime dateTime, IFormatProvider? provider, TimeZoneInfo timeZone) => dateTime.ToTimeZone(timeZone).ToString(provider);

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent string representation using the specified format and the formatting conventions of the current culture in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToString(this DateTime dateTime, string? format, TimeZoneInfo timeZone) => dateTime.ToTimeZone(timeZone).ToString(format);

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent string representation using the specified format and culture-specific format information in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToString(this DateTime dateTime, string? format, IFormatProvider? provider, TimeZoneInfo timeZone) => dateTime.ToTimeZone(timeZone).ToString(format, provider);

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent long date string representation in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToLongDateString(this DateTime dateTime, TimeZoneInfo timeZone) => dateTime.ToTimeZone(timeZone).ToLongDateString();

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent long time string representation in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToLongTimeString(this DateTime dateTime, TimeZoneInfo timeZone) => dateTime.ToTimeZone(timeZone).ToLongTimeString();

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent short date string representation in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToShortDateString(this DateTime dateTime, TimeZoneInfo timeZone) => dateTime.ToTimeZone(timeZone).ToShortDateString();

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent short time string representation in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToShortTimeString(this DateTime dateTime, TimeZoneInfo timeZone) => dateTime.ToTimeZone(timeZone).ToShortTimeString();

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent string representation using the formatting conventions of the current culture in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneString(this DateTime dateTime) => dateTime.ToString(TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent string representation using the specified culture-specific format information in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneString(this DateTime dateTime, IFormatProvider? provider) => dateTime.ToString(provider, TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent string representation using the specified format and the formatting conventions of the current culture in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneString(this DateTime dateTime, string? format) => dateTime.ToString(format, TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent string representation using the specified format and culture-specific format information in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneString(this DateTime dateTime, string? format, IFormatProvider? provider) => dateTime.ToString(format, provider, TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent long date string representation in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneLongDateString(this DateTime dateTime) => dateTime.ToLongDateString(TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent long time string representation in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneLongTimeString(this DateTime dateTime) => dateTime.ToLongTimeString(TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent short date string representation in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneShortDateString(this DateTime dateTime) => dateTime.ToShortDateString(TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="DateTime" /> object to its equivalent short time string representation in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneShortTimeString(this DateTime dateTime) => dateTime.ToShortTimeString(TimeZoneInfo.Local);
    }
}
