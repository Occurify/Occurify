
namespace Occurify.TimeZones.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Period"/>.
    /// </summary>
    public static class PeriodExtensions
    {
        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent string representation using the formatting conventions of the current culture in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToString(this Period period, TimeZoneInfo timeZone) => period.ToString(dt => dt.ToTimeZone(timeZone).ToString(null, null));

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent string representation using the specified culture-specific format information in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToString(this Period period, IFormatProvider? provider, TimeZoneInfo timeZone) => period.ToString(dt => dt.ToTimeZone(timeZone).ToString(provider));

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent string representation using the specified format and the formatting conventions of the current culture in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToString(this Period period, string? format, TimeZoneInfo timeZone) => period.ToString(dt => dt.ToTimeZone(timeZone).ToString(format));

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent string representation using the specified format and culture-specific format information in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToString(this Period period, string? format, IFormatProvider? provider, TimeZoneInfo timeZone) => period.ToString(dt => dt.ToTimeZone(timeZone).ToString(format, provider));

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent long date string representation in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToLongDateString(this Period period, TimeZoneInfo timeZone) => period.ToString(dt => dt.ToTimeZone(timeZone).ToLongDateString());

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent long time string representation in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToLongTimeString(this Period period, TimeZoneInfo timeZone) => period.ToString(dt => dt.ToTimeZone(timeZone).ToLongTimeString());

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent short date string representation in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToShortDateString(this Period period, TimeZoneInfo timeZone) => period.ToString(dt => dt.ToTimeZone(timeZone).ToShortDateString());

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent short time string representation in timezone <paramref name="timeZone"/>.
        /// </summary>
        public static string ToShortTimeString(this Period period, TimeZoneInfo timeZone) => period.ToString(dt => dt.ToTimeZone(timeZone).ToShortTimeString());

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent string representation using the formatting conventions of the current culture in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneString(this Period period) => period.ToString(TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent string representation using the specified culture-specific format information in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneString(this Period period, IFormatProvider? provider) => period.ToString(provider, TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent string representation using the specified format and the formatting conventions of the current culture in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneString(this Period period, string? format) => period.ToString(format, TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent string representation using the specified format and culture-specific format information in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneString(this Period period, string? format, IFormatProvider? provider) => period.ToString(format, provider, TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent long date string representation in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneLongDateString(this Period period) => period.ToLongDateString(TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent long time string representation in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneLongTimeString(this Period period) => period.ToLongTimeString(TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent short date string representation in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneShortDateString(this Period period) => period.ToShortDateString(TimeZoneInfo.Local);

        /// <summary>
        /// Converts the value of the current <see cref="Period" /> object to its equivalent short time string representation in timezone <see cref="TimeZoneInfo.Local" />.
        /// </summary>
        public static string ToLocalTimeZoneShortTimeString(this Period period) => period.ToShortTimeString(TimeZoneInfo.Local);

        private static string ToString(this Period period, Func<DateTime, string> dateTimeToStringFunc)
        {
            if (period.IsInfiniteInBothDirections)
            {
                return "∞";
            }

            if (period.HasAlwaysStarted)
            {
                return $"∞<->{dateTimeToStringFunc(period.End.Value)}";
            }

            if (period.NeverEnds)
            {
                return $"{dateTimeToStringFunc(period.Start.Value)}<->∞";
            }

            return $"{dateTimeToStringFunc(period.Start.Value)}<->{dateTimeToStringFunc(period.End.Value)}";
        }
    }
}
