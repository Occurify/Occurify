
namespace Occurify.TimeZones.Extensions
{
    public static class PeriodExtensions
    {
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
