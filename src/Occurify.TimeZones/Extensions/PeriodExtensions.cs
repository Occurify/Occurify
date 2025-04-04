﻿
namespace Occurify.TimeZones.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Period"/>.
    /// </summary>
    public static class PeriodExtensions
    {
        /// <summary>
        /// Returns a string representation of the <paramref name="period"/> in the specified <paramref name="timeZone"/>.
        /// </summary>
        public static string ToString(this Period period, TimeZoneInfo timeZone)
        {
            if (period.IsInfiniteInBothDirections)
            {
                return "∞";
            }

            if (period.HasAlwaysStarted)
            {
                return $"∞<->{TimeZoneInfo.ConvertTimeFromUtc(period.End.Value, timeZone)}";
            }

            if (period.NeverEnds)
            {
                return $"{TimeZoneInfo.ConvertTimeFromUtc(period.Start.Value, timeZone)}<->∞";
            }

            return $"{TimeZoneInfo.ConvertTimeFromUtc(period.Start.Value, timeZone)}<->{TimeZoneInfo.ConvertTimeFromUtc(period.End.Value, timeZone)}";
        }
    }
}
