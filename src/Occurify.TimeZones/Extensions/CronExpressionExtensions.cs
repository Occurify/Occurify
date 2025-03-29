using Cronos;

namespace Occurify.TimeZones.Extensions
{
    internal static class CronExpressionExtensions
    {
        internal static TimeSpan? GetFirstPeriodDuration(this CronExpression cronExpression, TimeZoneInfo timeZone)
        {
            var first = cronExpression.GetNextOccurrence(new (0, DateTimeKind.Utc), timeZone, inclusive: true);
            if (first == null)
            {
                return null;
            }

            var second = cronExpression.GetNextOccurrence(first.Value, timeZone, inclusive: false);
            if (second == null)
            {
                return null;
            }

            return second.Value - first.Value;
        }
    }
}
