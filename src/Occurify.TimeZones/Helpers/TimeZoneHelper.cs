namespace Occurify.TimeZones.Helpers
{
    internal static class TimeZoneHelper
    {
        /// <summary>
        /// Returns the UTC instant of local noon on the date of <paramref name="dateTime"/> in <paramref name="timeZone"/>.
        /// Noon is used as the anchor for "the day of this date" because local midnight does not exist in zones whose
        /// daylight saving transition happens at 00:00, and because the <see cref="DateTime.Kind"/> of the input is irrelevant.
        /// </summary>
        internal static DateTime NoonOfDateToUtc(DateTime dateTime, TimeZoneInfo timeZone)
        {
            var localNoon = DateTime.SpecifyKind(dateTime.Date, DateTimeKind.Unspecified).AddHours(12);
            return TimeZoneInfo.ConvertTimeToUtc(localNoon, timeZone);
        }
    }
}
